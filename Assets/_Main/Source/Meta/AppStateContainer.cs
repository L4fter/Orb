using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppStateContainer : IAppControls
{
    private AppState currentState;
    private IGameProvider gameProvider;

    public AppStateContainer(IGameProvider gameProvider)
    {
        this.gameProvider = gameProvider;
    }

    public void StartGame()
    {
        if (currentState == AppState.GameNotStarted)
        {
            gameProvider.CreateNewGame();
            
            SceneManager.LoadScene("Prototype");
        }
    }

    public bool CanLoadAndContinue => false;

    public void LoadGameAndContinue()
    {
        throw new NotImplementedException();
    }


    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void ExitToOs()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

interface IGamePauseControls
{
    void PauseToMenu();
    void ResumeGame();
    void RestartGame();
}

public enum AppState
{
    GameNotStarted,
    GameInProgress,
    GameOnPause,
}
