using Meta.PoorMansDi;

public class GameProvider : IGameProvider
{
    private IResolver resolver;

    public GameProvider(IResolver resolver)
    {
        this.resolver = resolver;
    }
    
    public Game CurrentGame { get; private set; }
    
    public void CreateNewGame()
    {
        CurrentGame?.Discard();

        CurrentGame = resolver.Resolve<Game>();
        CurrentGame.WaitForViewReady();
    }
}