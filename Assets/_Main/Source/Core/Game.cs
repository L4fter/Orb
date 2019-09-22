using System.Linq;
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
    private CelestialSystem celestialSystem;
    private IPlanetFactory planetFactory;
    private IProjectileFactory projectileFactory;

    private float timeOfSerialization;

    public Game(IWinLoseHandler winLoseHandler, IResolver resolver, InputPlanetController inputPlanetController, 
        AiPlanetController aiPlanetController, ISerializedCelestialSystemProvider csProvider, ICelestialSystemSerializer serializer)
    {
        this.winLoseHandler = winLoseHandler;
        this.resolver = resolver;
        this.inputPlanetController = inputPlanetController;
        this.aiPlanetController = aiPlanetController;
        this.csProvider = csProvider;
        this.serializer = serializer;
    }

    public void MakeTimestep(float deltaTime)
    {
        celestialSystem.SimulateTimestep(deltaTime);

//        if (celestialSystem.IsPlayerAlive &&
//            !celestialSystem.GetAliveAiPlanets().Any())
//        {
//            //winLoseHandler.Win();
//            serializer.Clear();
//            return;
//        }

//        if (!celestialSystem.IsPlayerAlive)
//        {
//            winLoseHandler.Lose();
//            serializer.Clear();
//            return;
//        }

        if (timeOfSerialization + 1 < Time.time)
        {
            serializer.Serialize(celestialSystem);
        }
        
    }

    public void OnViewReady()
    {
        Debug.Log("View ready");
//        serializer.Clear();
        
        // Injecting stuff that couldnt be created before the game
        // Mostly from scene, which is loaded later at unknown time
        planetFactory = resolver.Resolve<IPlanetFactory>();
        projectileFactory = resolver.Resolve<IProjectileFactory>();

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
        celestialSystem = csProvider.CreateFromSerialized(projectileFactory, planetFactory);
        var aiPlanet = celestialSystem.Planets[csProvider.AiControlledPlanetIndex];
        aiPlanetController.Control(aiPlanet, projectileFactory);

        var playerPlanet = celestialSystem.Planets[csProvider.PlayerControlledPlanetIndex];
        inputPlanetController.Control(playerPlanet, projectileFactory);
    }

    private void CreateCelestialSystemFromScene()
    {
        celestialSystem = resolver.Resolve<CelestialSystem>();
        var planets = planetFactory.CollectAllAvailablePlanets();
        var centralStar = planets.OrderByDescending(planet => planet.SimulatedEntity.Mass).First();
        
        celestialSystem.AddCentralStar(centralStar);
        celestialSystem.Add(planets);

        var onlyPlanets = planets.Except(new[] {centralStar}).ToList();
        var randomIndex = Random.Range(0, onlyPlanets.Count);
        var randomPlanet = onlyPlanets.Skip(randomIndex).First();
        inputPlanetController.Control(randomPlanet, projectileFactory);

        onlyPlanets.Remove(randomPlanet);
        
        randomIndex = Random.Range(0, onlyPlanets.Count);
        randomPlanet = onlyPlanets.Skip(randomIndex).First();
        aiPlanetController.Control(randomPlanet, projectileFactory);
    }

    public void Discard()
    {
        
    }

    public void WaitForViewReady()
    {
        // just chill
    }
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
    CelestialSystem CreateFromSerialized(IProjectileFactory projectileFactory, IPlanetFactory planetFactory);
}