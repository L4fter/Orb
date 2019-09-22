using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Meta.PoorMansDi;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class InGameHud : DiMonoBehaviour, IGameHudController
{
    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private GameObject winPanel;
    
    private IWinLoseHandler winLoseHandler;
    private GameHudProvider gameHudProvider;

    private TaskCompletionSource<bool> tcsForWinLostPanel;

    private void Start()
    {
        gameHudProvider.Register(this);
    }

    private void OnDestroy()
    {
        gameHudProvider?.Unregister(this);
    }

    [UsedImplicitly]
    public void Init(IWinLoseHandler winLoseHandler, GameHudProvider gameHudProvider)
    {
        this.winLoseHandler = winLoseHandler;
        this.gameHudProvider = gameHudProvider;
    }
    
    [Conditional("DEBUG")]
    public void DebugLose()
    {
        winLoseHandler.Lose();
    }

    [Conditional("DEBUG")]
    public void DebugWin()
    {
        winLoseHandler.Win();
    }

    public async Task<bool> ShowWinMenu()
    {
        Debug.Log("ShowWin");
        
        winPanel.SetActive(true);
        tcsForWinLostPanel = new TaskCompletionSource<bool>();
        
        return await tcsForWinLostPanel.Task;
    }

    public async Task<bool> ShowLoseMenu()
    {
        Debug.Log("ShowLose");
        
        losePanel.SetActive(true);
        tcsForWinLostPanel = new TaskCompletionSource<bool>();
        
        return await tcsForWinLostPanel.Task;
    }

    private void CloseWinLosePanelWithResult(bool shouldRestart)
    {
        tcsForWinLostPanel.SetResult(shouldRestart);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void Restart()
    {
        CloseWinLosePanelWithResult(true);
    }

    public void ToMenu()
    {
        tcsForWinLostPanel.SetResult(false);
    }
}