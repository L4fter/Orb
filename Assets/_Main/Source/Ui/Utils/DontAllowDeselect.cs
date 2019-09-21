using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DontAllowDeselect : MonoBehaviour
{

    public GameObject selectIfNothingIsSelected;
    
    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.currentSelectedGameObject)
        {
            EventSystem.current.SetSelectedGameObject(selectIfNothingIsSelected);
        }
    }
}
