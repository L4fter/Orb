using Core;
using UnityEngine;

public class InputPlanetController : PlanetController, IInputReceiver
{
    public void Fire(Vector2 target)
    {
        var pos2d = planet.SimulatedEntity.Position;
        var direction = (target - pos2d).normalized;
        Debug.DrawLine(pos2d, pos2d + direction, Color.red);
        
        ShootAtDirection(direction);
    }

    public override void Control(IPlanet randomPlanet, IWeapon weapon)
    {
        base.Control(randomPlanet, weapon);
        planet.ControlledByPlayer = true;
    }
}