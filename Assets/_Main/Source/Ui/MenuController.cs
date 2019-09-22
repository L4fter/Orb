using System;
using JetBrains.Annotations;
using Meta.PoorMansDi;
using UnityEngine;

public class MenuController : DiMonoBehaviour
{
    private IAppControls appControls;
    private ISerializedCelestialSystemProvider csProvider;

    [SerializeField] 
    private GameObject continueButton;

    [UsedImplicitly]
    public void Init(IAppControls appControls, ISerializedCelestialSystemProvider csProvider)
    {
        this.csProvider = csProvider;
        this.appControls = appControls;
    }

    public void Start()
    {
        continueButton.SetActive(csProvider.HasSerializedCelestialSystem);
    }

    public void StartGame()
    {
        appControls.StartGame();
    }
    
    public void LoadGame()
    {
        appControls.LoadGameAndContinue();
    }
    
    public void ExitGame()
    {
        appControls.ExitToOs();
    }
}
