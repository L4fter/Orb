using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Meta.PoorMansDi;
using UnityEngine;

public class Serializer : ICelestialSystemSerializer, ISerializedCelestialSystemProvider
{
    private readonly IResolver resolver;
    private const string saveGameKey = "savegame";
    private SerializedCs scs;

    public Serializer(IResolver resolver)
    {
        this.resolver = resolver;
        TryLoad();
    }

    public void Serialize(CelestialSystem cs)
    {
        scs = new SerializedCs();
        var planets = cs.Planets;
        scs.playerIndex = planets.FindIndex(planet => planet.ControlledBy is InputPlanetController);
        scs.aiIndex = planets.FindIndex(planet => planet.ControlledBy is AiPlanetController);

        scs.planets = new List<SerializedPlanet>();
        foreach (var planet in planets)
        {
            scs.planets.Add(new SerializedPlanet()
            {
                entity = SerializedEntity.FromEntity(planet.SimulatedEntity),
                hp = planet.Hp,
                startHp = planet.StartHp,
            });
        }
        
        scs.planets.Add(new SerializedPlanet()
        {
            entity = SerializedEntity.FromEntity(cs.centralStar.SimulatedEntity),
            hp = cs.centralStar.Hp,
            startHp = cs.centralStar.StartHp,
            isCentral = true,
        });
        
        
        scs.projectiles = new List<SerializedProjectile>();
        foreach (var projectile in cs.Projectiles)
        {
            scs.projectiles.Add(new SerializedProjectile()
            {
                entity = SerializedEntity.FromEntity(projectile),
            });
        }

        var json = JsonUtility.ToJson(scs);
        PlayerPrefs.SetString(saveGameKey, json);
        PlayerPrefs.Save();
    }

    public void Clear()
    {
        scs = null;
        PlayerPrefs.SetString(saveGameKey, null);
        PlayerPrefs.Save();
    }

    private void TryLoad()
    {
        var savedJson = PlayerPrefs.GetString(saveGameKey);
        if (string.IsNullOrEmpty(savedJson))
        {
            scs = null;
        }
        else
        {
            scs = JsonUtility.FromJson<SerializedCs>(savedJson);
        }
    }

    public bool HasSerializedCelestialSystem => scs != null;
    public int AiControlledPlanetIndex => scs.aiIndex;
    public int PlayerControlledPlanetIndex => scs.playerIndex;
    public CelestialSystem CreateFromSerialized(IProjectileFactory projectileFactory, IPlanetFactory planetFactory)
    {
        var celestialSystem = resolver.Resolve<CelestialSystem>();
        var planets = new List<IPlanet>();
        
        var central = scs.planets.First(planet => planet.isCentral);
        var centralStarPlanet = planetFactory.CreatePlanet(central.entity.pos);
        central.entity.FillIn(centralStarPlanet.SimulatedEntity);
        celestialSystem.AddCentralStar(centralStarPlanet);

        foreach (var serializedPlanet in scs.planets)
        {
            var planet = planetFactory.CreatePlanet(serializedPlanet.entity.pos);
            serializedPlanet.entity.FillIn(planet.SimulatedEntity);
            planets.Add(planet);
        }
        celestialSystem.AddRaw(planets);
        projectileFactory.celestialSystem = celestialSystem;
        
        foreach (var projectile in scs.projectiles)
        {
            var proj = projectileFactory.CreateBullet(projectile.entity.pos);
            projectile.entity.FillIn(proj);
        }

        return celestialSystem;
    }
}

[Serializable]
internal class SerializedCs
{
    public int playerIndex;
    public int aiIndex;
    public List<SerializedPlanet> planets;
    public List<SerializedProjectile> projectiles;
}

[Serializable]
internal class SerializedPlanet
{
    public SerializedEntity entity;
    public int hp;
    public int startHp;
    public bool isCentral;
}

[Serializable]
internal class SerializedProjectile
{
    public SerializedEntity entity;
}

[Serializable]
internal class SerializedEntity
{
    public static SerializedEntity FromEntity(ISimulatedEntity entity)
    {
        return new SerializedEntity()
        {
            pos = entity.Position,
            vel = entity.Velocity,
            acc = entity.Acceleration,
            timeOfAcc = entity.TimeForAcceleration,
            mass = entity.Mass,
            isAttractedByOthers = entity.IsAttractedByOthers,
            attractsOthers = entity.AttractsOthers
        };
    }

    public void FillIn(ISimulatedEntity entity)
    {
        entity.Position = pos;
        entity.Velocity = vel;
        entity.Acceleration = acc;
        entity.TimeForAcceleration = timeOfAcc;
        entity.Mass = mass;
        entity.IsAttractedByOthers = isAttractedByOthers;
        entity.AttractsOthers = attractsOthers;
    }
    
    public Vector2 pos;
    public Vector2 vel;
    public Vector2 acc;
    public float timeOfAcc;
    public float mass;
    public bool isAttractedByOthers;
    public bool attractsOthers;
}