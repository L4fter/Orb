using System.Collections;
using System.Collections.Generic;
using Meta.PoorMansDi;
using UnityEngine;

public class ViewReady : DiMonoBehaviour
{
    private IGameControls gameControls;

    public void Init(IGameControls gameControls)
    {
        this.gameControls = gameControls;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameControls.CurrentGame.OnViewReady();
    }
}

public interface IGameControls
{
    Game CurrentGame { get; }
    void CreateNewGame();
}

public class GameControls : IGameControls
{
    private IResolver resolver;

    public GameControls(IResolver resolver)
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
