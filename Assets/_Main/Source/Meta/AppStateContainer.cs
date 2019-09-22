using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppStateContainer : IAppControls
{
    private AppState currentState;
    private IGameProvider gameProvider;
    private readonly ICelestialSystemSerializer csSerializer;

    public AppStateContainer(IGameProvider gameProvider, ICelestialSystemSerializer csSerializer)
    {
        this.gameProvider = gameProvider;
        this.csSerializer = csSerializer;
    }

    public void StartGame()
    {
        if (currentState == AppState.GameNotStarted)
        {
            csSerializer.Clear();
            gameProvider.CreateNewGame();
            
            SceneManager.LoadScene("Prototype");
        }
    }

    public bool CanLoadAndContinue => false;

    public void LoadGameAndContinue()
    {
        gameProvider.CreateNewGame();
        SceneManager.LoadScene("EmptyGame");
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
