using System;
using System.Threading.Tasks;

public class GameHudProvider : IGameHudController
{
    private InGameHud gameHud;

    internal void Register(InGameHud hud)
    {
        gameHud = hud;
    }

    internal void Unregister(InGameHud hud)
    {
        if (gameHud == hud)
        {
            gameHud = null;
        }
    }
    
    public Task<bool> ShowWinMenu()
    {
        CheckExists();

        return gameHud.ShowWinMenu();
    }

    public Task<bool> ShowLoseMenu()
    {
        CheckExists();

        return gameHud.ShowLoseMenu();
    }

    private void CheckExists()
    {
        if (gameHud == null)
        {
            throw new Exception("Game hud doesn't exist yet");
        }
    }
}