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
        
        binder.Bind<IGameControls>().ToSingle<GameControls>();
        binder.Bind<Game>().ToFactoryOf<Game>();
        
        binder.Bind<IGameHudController>().ToSingle<GameHudProvider>();
        binder.Bind<GameHudProvider>().ToSingle<GameHudProvider>();
    }

}
