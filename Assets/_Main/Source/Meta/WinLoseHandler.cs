using System.Threading.Tasks;

public class WinLoseHandler : IWinLoseHandler
{
    private readonly IAppControls appControls;
    private readonly IGameHudController gameHudController;

    public WinLoseHandler(IAppControls appControls, IGameHudController gameHudController)
    {
        this.appControls = appControls;
        this.gameHudController = gameHudController;
    }

    public async void Win()
    {
        var shouldRestart = await gameHudController.ShowWinMenu();
        ExitAndRestartIfNeeded(shouldRestart);
        // Show win text
        // return to menu
    }

    private void ExitAndRestartIfNeeded(bool shouldRestart)
    {
        appControls.ExitToMenu();

        if (shouldRestart)
        {
            appControls.StartGame();
        }
    }

    public async void Lose()
    {
        var shouldRestart = await gameHudController.ShowLoseMenu();
        ExitAndRestartIfNeeded(shouldRestart);
    }
}

public interface IGameHudController
{
    Task<bool> ShowWinMenu();
    Task<bool> ShowLoseMenu();
}