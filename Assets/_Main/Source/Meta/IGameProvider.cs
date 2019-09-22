public interface IGameProvider
{
    Game CurrentGame { get; }
    void CreateNewGame();
}