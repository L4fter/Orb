using System.Linq;
using Core;
using Meta.PoorMansDi;
using UnityEngine;

public class Game
{
    private IWinLoseHandler winLoseHandler;
    private readonly IResolver resolver;
    private readonly InputPlanetController inputPlanetController;
    private readonly AiPlanetController aiPlanetController;
    private readonly ISerializedCelestialSystemProvider csProvider;
    private readonly ICelestialSystemSerializer serializer;
    private readonly PlayerStatsProvider playerStatsProvider;
    private IWeaponFactory weaponFactory;
    private CelestialSystem celestialSystem;
    private IPlanetFactory planetFactory;
    private IProjectileFactory projectileFactory;

    private float timeOfSerialization;

    public Game(IWinLoseHandler winLoseHandler, IResolver resolver, InputPlanetController inputPlanetController, 
        AiPlanetController aiPlanetController, ISerializedCelestialSystemProvider csProvider, ICelestialSystemSerializer serializer,
        PlayerStatsProvider playerStatsProvider)
    {
        this.winLoseHandler = winLoseHandler;
        this.resolver = resolver;
        this.inputPlanetController = inputPlanetController;
        this.aiPlanetController = aiPlanetController;
        this.csProvider = csProvider;
        this.serializer = serializer;
        this.playerStatsProvider = playerStatsProvider;
    }

    public void MakeTimestep(float deltaTime)
    {
        celestialSystem.SimulateTimestep(deltaTime);

        if (inputPlanetController.Planet.Hp > 0 &&
            aiPlanetController.Planet.Hp <= 0)
        {
            winLoseHandler.Win();
            return;
        }

        if (inputPlanetController.Planet.Hp <= 0)
        {
            winLoseHandler.Lose();
            return;
        }

        if (timeOfSerialization + 1 < Time.time)
        {
            serializer.Serialize(celestialSystem);
        }
        
    }

    public void OnViewReady()
    {
        Debug.Log("View ready");
        
        // Injecting stuff that couldnt be created before the game
        // Mostly from scene, which is loaded later at unknown time
        planetFactory = resolver.Resolve<IPlanetFactory>();
        projectileFactory = resolver.Resolve<IProjectileFactory>();
        weaponFactory = resolver.Resolve<IWeaponFactory>();

        if (csProvider.HasSerializedCelestialSystem)
        {
            CreateCelestialSystemFromSerialized();
        }
        else
        {
            CreateCelestialSystemFromScene();
        }
        
        projectileFactory.celestialSystem = celestialSystem;
    }

    private void CreateCelestialSystemFromSerialized()
    {
        celestialSystem = csProvider.CreateFromSerialized(projectileFactory, planetFactory, weaponFactory);
        var aiPlanet = celestialSystem.Planets[csProvider.AiControlledPlanetIndex];
        aiPlanetController.Control(aiPlanet, aiPlanet.Weapon);

        var playerPlanet = celestialSystem.Planets[csProvider.PlayerControlledPlanetIndex];
        inputPlanetController.Control(playerPlanet, playerPlanet.Weapon);
        playerStatsProvider.SetPlayerPlanet(playerPlanet);
    }

    private void CreateCelestialSystemFromScene()
    {
        celestialSystem = resolver.Resolve<CelestialSystem>();
        var planets = planetFactory.CollectAllAvailablePlanets();
        var centralStar = planets.OrderByDescending(planet => planet.SimulatedEntity.Mass).First();
        
        celestialSystem.AddCentralStar(centralStar);
        celestialSystem.Add(planets);

        var allAvailableWeapons = weaponFactory.GetAllAvailableTypes();

        var onlyPlanets = planets.Except(new[] {centralStar}).ToList();
        var randomIndex = Random.Range(0, onlyPlanets.Count);

        var randomPlanet = onlyPlanets.Skip(randomIndex).First();
        var randomWeapon = allAvailableWeapons[Random.Range(0, allAvailableWeapons.Count)];
        weaponFactory.AddWeapon(randomWeapon, randomPlanet);
        inputPlanetController.Control(randomPlanet, randomPlanet.Weapon);
        playerStatsProvider.SetPlayerPlanet(randomPlanet);

        onlyPlanets.Remove(randomPlanet);
        
        randomIndex = Random.Range(0, onlyPlanets.Count);
        randomPlanet = onlyPlanets.Skip(randomIndex).First();
        randomWeapon = allAvailableWeapons[Random.Range(0, allAvailableWeapons.Count)];
        weaponFactory.AddWeapon(randomWeapon, randomPlanet);
        aiPlanetController.Control(randomPlanet, randomPlanet.Weapon);
    }

    public void Discard()
    {
        playerStatsProvider.SetPlayerPlanet(null);
    }

    public void WaitForViewReady()
    {
        // just chill
    }
}

public class PlayerStatsProvider : IPlayerStatsProvider
{
    private IPlanet planet;

    public void SetPlayerPlanet(IPlanet planet)
    {
        this.planet = planet;
    }


    public float HpPercentage => planet == null ? 1 : planet.Hp/(float)planet.StartHp;
    public float ReloadPercentage => planet?.Weapon.ReloadAmount ?? 1;
} 

public interface IPlayerStatsProvider
{
    float HpPercentage { get; }
    float ReloadPercentage { get; }
}

public interface ICelestialSystemSerializer
{
    void Serialize(CelestialSystem cs);
    void Clear();
}

public interface ISerializedCelestialSystemProvider
{
    bool HasSerializedCelestialSystem { get; }
    int AiControlledPlanetIndex { get; }
    int PlayerControlledPlanetIndex { get;}
    CelestialSystem CreateFromSerialized(IProjectileFactory projectileFactory, IPlanetFactory planetFactory,
        IWeaponFactory weaponFactory);
}