using System.Collections;
using System.Collections.Generic;
using Meta.PoorMansDi;
using UnityEngine;

public class ViewReady : DiMonoBehaviour
{
    private IGameProvider gameProvider;

    public void Init(IGameProvider gameProvider)
    {
        this.gameProvider = gameProvider;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameProvider.CurrentGame.OnViewReady();
    }
}