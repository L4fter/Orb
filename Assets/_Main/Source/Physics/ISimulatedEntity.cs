using UnityEngine;

public interface ISimulatedEntity
{
    bool IsAttractedByOthers { get; }
    bool AttractsOthers { get; }
    float Mass{ get; }
    Vector2 Position { get; set; }
    Vector2 Velocity { get; set; }
    Vector2 Acceleration { get; }
    float TimeForAcceleration { get; set; }
}