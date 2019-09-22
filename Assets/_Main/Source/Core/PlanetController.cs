using UnityEngine;

public class PlanetController
{
    protected IPlanet planet;
    protected IProjectileFactory factory;

    public IPlanet Planet => planet;
    protected void ShootAtDirection(Vector2 direction)
    {
        Vector2 pos2d = planet.SimulatedEntity.Position;
        var distanceFromCenter = 1f;
        var bullet = factory.CreateBullet(pos2d + direction * distanceFromCenter);
        bullet.Velocity = planet.SimulatedEntity.Velocity;
        bullet.Acceleration = direction * 4;
        bullet.TimeForAcceleration = 0.2f;
    }

    public virtual void Control(IPlanet randomPlanet, IProjectileFactory projectileFactory)
    {
        factory = projectileFactory;
        this.planet = randomPlanet;
        planet.ControlledBy = this;
        
    }
}