using JetBrains.Annotations;
using Meta.PoorMansDi;

public class MenuController : DiMonoBehavior
{
    private IAppControls appControls;
    
    [UsedImplicitly]
    public void Init(IAppControls appControls)
    {
        this.appControls = appControls;
    }
    
    public void StartGame()
    {
        appControls.StartGame();
    }
    
    public void ExitGame()
    {
        appControls.ExitFromGame();
    }
}
