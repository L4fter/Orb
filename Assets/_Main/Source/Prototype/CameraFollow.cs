using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followee;
    public Transform directionHold;

    private Vector3 delta;
    private Vector2 direction;
    
    void Start()
    {
        delta = this.transform.position - followee.transform.position;
        if (directionHold)
        {
            direction = (followee.transform.position - directionHold.position).normalized;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = followee.transform.position + delta;
        if (directionHold)
        {
            this.transform.rotation = Quaternion.FromToRotation(direction, (followee.transform.position - directionHold.position).normalized);
        }
    }
}
