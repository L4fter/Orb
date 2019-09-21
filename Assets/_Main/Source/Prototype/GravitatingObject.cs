using System.Linq;
using UnityEngine;

public class GravitatingObject : MonoBehaviour
{
    public const float GConstant = 0.0005f;
    
    public Vector2 velocity;
    public float Mass = 1;

    public bool attractsOthers = false;
    public bool isAttractedByOthers = true;
    public Vector2 acceleration;

    public float accelerationTime = 1f;

    public void StartAcceleration(Vector2 acc, float time)
    {
        acceleration = acc;
        accelerationTime = time;
    }

    void FixedUpdate()
    {
        if (accelerationTime > 0)
        {
            velocity += acceleration;
            accelerationTime -= Time.fixedDeltaTime;
        }
        
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
            var r2 = Mathf.Max(direction.sqrMagnitude, 0.00001f);
            var deltaV = direction.normalized * (GConstant * celestialBody.Mass / r2);

            totalDeltaV += deltaV;
        }
        
        var totalDeltaV2d = Vector2.ClampMagnitude(new Vector2(totalDeltaV.x, totalDeltaV.y), maxDeltaV);

        this.velocity += totalDeltaV2d;
        
        this.transform.Translate(velocity * Time.fixedDeltaTime);
    }
}
