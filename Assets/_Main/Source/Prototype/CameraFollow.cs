using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followee;

    private Vector3 delta;
    
    void Start()
    {
        delta = this.transform.position - followee.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = followee.transform.position + delta;
    }
}
