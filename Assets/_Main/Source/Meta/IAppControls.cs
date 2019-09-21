public interface IAppControls
{
    void StartGame();

    bool CanLoadAndContinue { get; }
    void LoadGameAndContinue();
    void ExitFromGame();
    void LoadMenu();
}