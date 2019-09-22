using System;
using Meta.PoorMansDi;
using UnityEngine;

public class DiContainer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SimpleDi simpleDi = new SimpleDi();
        var binder = simpleDi.Binder;

        binder.Bind<IAppControls>().ToSingle<AppStateContainer>();
        binder.Bind<IWinLoseHandler>().ToSingle<WinLoseHandler>();
        
        binder.Bind<IGameProvider>().ToSingle<GameProvider>();
        binder.Bind<Game>().ToFactoryOf<Game>();
        binder.Bind<CelestialSystem>().ToFactoryOf<CelestialSystem>();
        binder.Bind<Solver>().ToFactoryOf<Solver>();
        binder.Bind<InputPlanetController>().ToSingle<InputPlanetController>();
        binder.Bind<AiPlanetController>().ToSingle<AiPlanetController>();
        binder.Bind<IInputReceiver>().ToSingle<InputPlanetController>();
        binder.Bind<IUpdatesReceiver>().ToSingle<AiPlanetController>();
        
        binder.Bind<IGameHudController>().ToSingle<GameHudProvider>();
        binder.Bind<GameHudProvider>().ToSingle<GameHudProvider>();
    }
}
