using System.Threading.Tasks;

public class WinLoseHandler : IWinLoseHandler
{
    private readonly IAppControls appControls;
    private readonly IGameHudController gameHudController;
    private readonly ICelestialSystemSerializer serializer;

    public WinLoseHandler(IAppControls appControls, IGameHudController gameHudController, ICelestialSystemSerializer serializer)
    {
        this.appControls = appControls;
        this.gameHudController = gameHudController;
        this.serializer = serializer;
    }

    public async void Win()
    {
        serializer.Clear();
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
        serializer.Clear();
        var shouldRestart = await gameHudController.ShowLoseMenu();
        ExitAndRestartIfNeeded(shouldRestart);
    }
}

public interface IGameHudController
{
    Task<bool> ShowWinMenu();
    Task<bool> ShowLoseMenu();
}