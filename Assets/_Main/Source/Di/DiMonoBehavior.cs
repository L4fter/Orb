using System;
using UnityEngine;

namespace Meta.PoorMansDi
{
    public class DiMonoBehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            if (!LoaderSceneChecker.wasLoaderSceneLoaded)
            {
                return;
            }
            DiBase.Injector.InjectInto(this);
        }
    }
}