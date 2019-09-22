using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GravitatingObject : MonoBehaviour, ISimulatedEntity
{
    [FormerlySerializedAs("Mass")]
    public float mass = 1;

    public bool attractsOthers = false;
    public bool isAttractedByOthers = true;
    
    public void StartAcceleration(Vector2 acc, float time)
    {
        Acceleration = acc;
        TimeForAcceleration = time;
    }

    void FixedUpdate()
    {
        this.transform.position = Position;
    }
    
    public bool IsAttractedByOthers
    {
        get => isAttractedByOthers;
        set => isAttractedByOthers = value;
    }

    public bool AttractsOthers
    {
        get => attractsOthers;
        set => attractsOthers = value;
    }

    public float Mass
    {
        get => this.mass;
        set => this.mass = value;
    }

    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public Vector2 Acceleration { get; set; }
    public float TimeForAcceleration { get; set; }
    public event Action Destroyed;

    public void DestroyEntity()
    {
        Destroyed?.Invoke();
    }
}
