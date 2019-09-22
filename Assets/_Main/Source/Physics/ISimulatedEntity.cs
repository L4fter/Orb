using System;
using UnityEngine;

public interface ISimulatedEntity
{
    bool IsAttractedByOthers { get; set; }
    bool AttractsOthers { get; }
    float Mass{ get; }
    Vector2 Position { get; set; }
    Vector2 Velocity { get; set; }
    Vector2 Acceleration { get; set; }
    float TimeForAcceleration { get; set; }

    event Action Destroyed;
}