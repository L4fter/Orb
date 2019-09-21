using System;
using UnityEngine;

namespace Meta.PoorMansDi
{
    public class DiMonoBehavior : MonoBehaviour
    {
        protected virtual void Awake()
        {
            SimpleDi.Injector.InjectInto(this);
        }
    }
}