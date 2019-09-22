using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solver
{
    private List<ISimulatedEntity> simulatedEntities = new List<ISimulatedEntity>();
    
    public const float GConstant = 0.005f;
    private float SimulationTimeScale = 2f;

    public void SimulateTimeStep(float dT)
    {
        for (int i = 0; i < simulatedEntities.Count; i++)
        {
            SimulateEntity(simulatedEntities[i], dT);
        }
    }

    private void SimulateEntity(ISimulatedEntity simulatedEntity, float dT)
    {
        var dVacceleration = CalculateAcceleration(simulatedEntity, dT);

        if (!simulatedEntity.IsAttractedByOthers)
        {
            return;
        }

        var dVgravity = CalculateGravity(simulatedEntity, dT);
        var dV = (dVacceleration + dVgravity);

        simulatedEntity.Velocity += SimulationTimeScale * dT * dV;
        simulatedEntity.Position += SimulationTimeScale * dT * simulatedEntity.Velocity;
    }

    private Vector2 CalculateGravity(ISimulatedEntity simulatedEntity, float dT)
    {
        var maxDeltaV = 5;
        var totalDeltaV = Vector2.zero;

        foreach (var celestialBody in simulatedEntities)
        {
            if (!celestialBody.AttractsOthers)
            {
                continue;
            }

            if (celestialBody == simulatedEntity)
            {
                continue;
            }

            totalDeltaV += CalcualteGravityDeltaVBetweenBodies(simulatedEntity, celestialBody);
        }

        totalDeltaV = Vector2.ClampMagnitude(totalDeltaV, maxDeltaV);
        return totalDeltaV;
    }

    private Vector2 CalcualteGravityDeltaVBetweenBodies(ISimulatedEntity attracted, ISimulatedEntity attractor)
    {
        var direction = attractor.Position - attracted.Position;
        var r2 = Mathf.Max(direction.sqrMagnitude, 0.00001f);
        
        var deltaV = (GConstant * attractor.Mass / r2) * direction.normalized;
        return deltaV;
    }

    private Vector2 CalculateAcceleration(ISimulatedEntity simulatedEntity, float dT)
    {
        if (simulatedEntity.TimeForAcceleration > 0)
        {
            simulatedEntity.TimeForAcceleration -= dT * SimulationTimeScale;
            return simulatedEntity.Acceleration;
        }
        
        return Vector2.zero;
    }

    public void AddEntity(ISimulatedEntity entity)
    {
        simulatedEntities.Add(entity);
    }

    public void RemoveEntity(ISimulatedEntity entity)
    {
        simulatedEntities.Remove(entity);
    }
}