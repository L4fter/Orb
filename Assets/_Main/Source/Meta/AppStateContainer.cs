using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppStateContainer : IAppControls
{
    private AppState currentState;
    private IGameControls gameControls;

    public AppStateContainer(IGameControls gameControls)
    {
        this.gameControls = gameControls;
    }

    public void StartGame()
    {
        if (currentState == AppState.GameNotStarted)
        {
            gameControls.CreateNewGame();
            
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
