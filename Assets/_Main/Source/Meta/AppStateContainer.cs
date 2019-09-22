using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppStateContainer : IAppControls
{
    private AppState currentState;

    public void StartGame()
    {
        if (currentState == AppState.GameNotStarted)
        {
            SceneManager.LoadScene("Prototype");
        }
    }

    public bool CanLoadAndContinue => false;

    public void LoadGameAndContinue()
    {
        throw new NotImplementedException();
    }

    public void ExitToOs()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
