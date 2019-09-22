using System;
using UnityEngine;

namespace Meta.PoorMansDi
{
    public class DiMonoBehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            DiBase.Injector.InjectInto(this);
        }
    }
}