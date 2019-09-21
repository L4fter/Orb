using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GravitatingObject : MonoBehaviour, ISimulatedEntity
{
    public const float GConstant = 0.005f;
    public Vector2 velocity;
    [FormerlySerializedAs("Mass")]
    public float mass = 1;

    public bool attractsOthers = false;
    public bool isAttractedByOthers = true;
    public Vector2 acceleration;

    public float accelerationTime = 0f;

    private void Start()
    {
        this.Position = this.transform.position;

        FindObjectOfType<GlobalSolver>().Solver.AddEntity(this);
    }

    private void OnDestroy()
    {
        FindObjectOfType<GlobalSolver>().Solver?.RemoveEntity(this);
    }

    public void StartAcceleration(Vector2 acc, float time)
    {
        Acceleration = acc;
        TimeForAcceleration = time;
    }

    void FixedUpdate()
    {
        this.transform.position = Position;
    }

    public void GetCircularOrbitAround(GravitatingObject other)
    {
        var radius = this.transform.position - other.transform.position;
        var mu = other.Mass * GConstant;
         
        var v = Mathf.Sqrt(mu / radius.magnitude);
        
        var tangent = new Vector2(radius.y, -radius.x).normalized;
        this.Velocity = tangent * v;
    }

    public bool IsAttractedByOthers => isAttractedByOthers;
    public bool AttractsOthers => attractsOthers;
    public float Mass => this.mass;
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public Vector2 Acceleration { get; private set; }
    public float TimeForAcceleration { get; set; }
}
