using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject BulletPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(BulletPrefab, this.transform.position + Vector3.one, Quaternion.identity);
            var gravitatingObject = BulletPrefab.GetComponent<GravitatingObject>();
            gravitatingObject.velocity = this.GetComponent<GravitatingObject>().velocity + Vector2.one * 2;
        }    
    }
}
