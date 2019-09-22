using System.Linq;
using Meta.PoorMansDi;
using UnityEngine;

public class Game
{
    private IWinLoseHandler winLoseHandler;
    private readonly IResolver resolver;
    private CelestialSystem celestialSystem;

    public Game(IWinLoseHandler winLoseHandler, IResolver resolver)
    {
        this.winLoseHandler = winLoseHandler;
        this.resolver = resolver;
    }

    public void MakeTimestep(float deltaTime)
    {
        celestialSystem.SimulateTimestep();

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
        // Mostly from scene, which is loaded later at unlnown time

        var planetFactory = resolver.Resolve<IPlanetFactory>();
        planetFactory.CreatePlanet(new Vector2(10, 10));
    }

    public void Discard()
    {
        
    }

    public void WaitForViewReady()
    {
        Debug.Log("Wait for view ready");
        // just chill
    }
}

internal class CelestialSystem
{
    public void SimulateTimestep()
    {
        
    }

    public bool IsPlayerAlive => true;

    public Planet[] GetAliveAiPlanets()
    {
        return new Planet[0];
    }
}

internal class Planet
{
}