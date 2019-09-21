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
    }

}
