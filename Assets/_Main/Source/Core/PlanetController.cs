using Core;
using UnityEngine;

public class PlanetController
{
    protected IPlanet planet;
    protected IWeapon weapon;

    public IPlanet Planet => planet;
    protected void ShootAtDirection(Vector2 direction)
    {
        weapon.Shoot(direction);
    }

    public virtual void Control(IPlanet randomPlanet, IWeapon weapon)
    {
        this.weapon = weapon;
        this.planet = randomPlanet;
        planet.ControlledBy = this;
        
    }
}