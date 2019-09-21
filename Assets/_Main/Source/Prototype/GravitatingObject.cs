using System.Linq;
using UnityEngine;

public class GravitatingObject : MonoBehaviour
{
    public const float GConstant = 0.001f;
    
    public Vector2 velocity;
    public float Mass = 1;

    public bool attractsOthers = false;
    public bool isAttractedByOthers = true;
    
    void FixedUpdate()
    {
        if (!isAttractedByOthers)
        {
            return;
        }
        
        var maxDeltaV = 5;
        var gravitatingObjects = FindObjectsOfType<GravitatingObject>();
        var totalDeltaV = Vector3.zero;

        foreach (var celestialBody in gravitatingObjects.Where(o => o.attractsOthers).Where(o => o != this))
        {
            var direction = celestialBody.transform.position - this.transform.position;
            var deltaV = direction.normalized * (GConstant * celestialBody.Mass / Mathf.Max(direction.sqrMagnitude, 0.00001f));

            totalDeltaV += deltaV;
        }
        
        var totalDeltaV2d = Vector2.ClampMagnitude(new Vector2(totalDeltaV.x, totalDeltaV.y), maxDeltaV);

        this.velocity += totalDeltaV2d;
        
        this.transform.Translate(velocity * Time.fixedDeltaTime);
    }
}
