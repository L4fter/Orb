public interface IAppControls
{
    void StartGame();

    bool CanLoadAndContinue { get; }
    void LoadGameAndContinue();
    void ExitToOs();
    void LoadMenu();
    void ExitToMenu();
}