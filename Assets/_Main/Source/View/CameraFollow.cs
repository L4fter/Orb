using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followee;
    public Transform directionHold;

    private Vector3 delta;
    private Vector2 direction;
    
    void Start()
    {
        var findObjectsOfType = FindObjectsOfType<Planet>();
        followee = findObjectsOfType.First(planet => planet.ControlledByPlayer).transform;
        this.transform.position = new Vector3(followee.position.x, followee.position.y, transform.position.z);
        
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
