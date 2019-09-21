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
            SceneManager.UnloadSceneAsync("MainMenu");
            SceneManager.LoadScene("Prototype");
        }
    }

    public bool CanLoadAndContinue => false;

    public void LoadGameAndContinue()
    {
        throw new NotImplementedException();
    }

    public void ExitFromGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
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
