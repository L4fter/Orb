using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Meta.PoorMansDi;
using UnityEngine;

public class InputHandler : DiMonoBehaviour
{
    private IInputReceiver inputReceiver;

    [UsedImplicitly]
    public void Init(IInputReceiver inputReceiver)
    {
        this.inputReceiver = inputReceiver;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var mousePos2d = new Vector2(mousePosition.x, mousePosition.y);
            inputReceiver.Fire(mousePos2d);
        }
    }
}
