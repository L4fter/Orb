using System;
using Meta.PoorMansDi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppStateContainer : DiMonoBehavior
{
    private AppState currentState;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        currentState = AppState.GameNotStarted;
        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void LoadPrototypeScene()
    {
        SceneManager.LoadScene("Prototype");
    }

    private void Update()
    {
        
    }
}

public enum AppState
{
    GameNotStarted,
    GameInProgress,
    GameOnPause,
}
