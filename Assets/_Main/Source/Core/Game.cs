using System.Linq;
using Meta.PoorMansDi;
using UnityEngine;

public class Game
{
    private IWinLoseHandler winLoseHandler;
    private readonly IResolver resolver;
    private CelestialSystem celestialSystem;
    private IPlanetFactory planetFactory;

    public Game(IWinLoseHandler winLoseHandler, IResolver resolver)
    {
        this.winLoseHandler = winLoseHandler;
        this.resolver = resolver;
    }

    public void MakeTimestep(float deltaTime)
    {
        celestialSystem.SimulateTimestep(deltaTime);

        if (celestialSystem.IsPlayerAlive &&
            !celestialSystem.GetAliveAiPlanets().Any())
        {
            winLoseHandler.Win();
            return;
        }

        if (!celestialSystem.IsPlayerAlive)
        {
            winLoseHandler.Lose();
        }
        
    }

    public void OnViewReady()
    {
        Debug.Log("View ready");
        
        // Injecting stuff that couldnt be created before the game
        // Mostly from scene, which is loaded later at unknown time
        planetFactory = resolver.Resolve<IPlanetFactory>();

        if (true)
        {
            CreateCelestialSystemFromScene();
        }
    }

    private void CreateCelestialSystemFromScene()
    {
        celestialSystem = new CelestialSystem();
        var planets = planetFactory.CollectAllAvailablePlanets();
        celestialSystem.Add(planets);
    }

    public void Discard()
    {
        
    }

    public void WaitForViewReady()
    {
        // just chill
    }
}