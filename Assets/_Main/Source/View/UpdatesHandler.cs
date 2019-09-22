using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Meta.PoorMansDi;
using UnityEngine;

public class UpdatesHandler : DiMonoBehaviour
{
    private IUpdatesReceiver updateReceiver;

    [UsedImplicitly]
    public void Init(IUpdatesReceiver updateReceiver)
    {
        this.updateReceiver = updateReceiver;
    }
    
    void Update()
    {
        updateReceiver.Update();    
    }
}
