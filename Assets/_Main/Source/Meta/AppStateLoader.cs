using JetBrains.Annotations;
using Meta.PoorMansDi;

public class AppStateLoader : DiMonoBehaviour
{
    private IAppControls appControls;

    [UsedImplicitly]
    public void Init(IAppControls appControls)
    {
        this.appControls = appControls;
        appControls.LoadMenu();
    }
    
    protected override void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        base.Awake();
    }
}