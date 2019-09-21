using System.Linq;
using UnityEngine;

public class GravitatingObject : MonoBehaviour
{
    public const float GConstant = 0.005f;
    public const float SimulationTimeScale = 3f;
    
    public Vector2 velocity;
    public float Mass = 1;

    public bool attractsOthers = false;
    public bool isAttractedByOthers = true;
    public Vector2 acceleration;

    public float accelerationTime = 0f;

    public void StartAcceleration(Vector2 acc, float time)
    {
        acceleration = acc;
        accelerationTime = time;
    }

    void FixedUpdate()
    {
        if (accelerationTime > 0)
        {
            velocity += SimulationTimeScale * Time.fixedDeltaTime * acceleration;
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
            var deltaV = (GConstant * celestialBody.Mass / r2) * direction.normalized;

            totalDeltaV += deltaV;
        }
        
        var totalDeltaV2d = Vector2.ClampMagnitude(new Vector2(totalDeltaV.x, totalDeltaV.y), maxDeltaV);

        this.velocity += Time.fixedDeltaTime * SimulationTimeScale * totalDeltaV2d;
        
        this.transform.Translate(Time.fixedDeltaTime * SimulationTimeScale * velocity);
    }

    public void GetCircularOrbitAround(GravitatingObject other)
    {
        var radius = this.transform.position - other.transform.position;
        var mu = other.Mass * GConstant;
         
        var v = Mathf.Sqrt(mu / radius.magnitude);
        
        var tangent = new Vector2(radius.y, -radius.x).normalized;
        this.velocity = tangent * v;
    }
}
