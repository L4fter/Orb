using System.Linq;

public interface IWinLoseHandler
{
    void Win();
    void Lose();
}

public class Game
{
    private IWinLoseHandler winLoseHandler;
    private CelestialSystem celestialSystem;

    public Game(IWinLoseHandler winLoseHandler)
    {
        this.winLoseHandler = winLoseHandler;
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