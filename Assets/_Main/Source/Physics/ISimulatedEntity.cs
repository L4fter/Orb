using System;
using UnityEngine;

public interface ISimulatedEntity
{
    bool IsAttractedByOthers { get; set; }
    bool AttractsOthers { get; set; }
    float Mass { get; set; }
    Vector2 Position { get; set; }
    Vector2 Velocity { get; set; }
    Vector2 Acceleration { get; set; }
    float TimeForAcceleration { get; set; }

    event Action Destroyed;
}