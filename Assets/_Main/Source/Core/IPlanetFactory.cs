using UnityEngine;

public interface IPlanetFactory
{
    IPlanet CreatePlanet(Vector2 position);

    IPlanet[] CollectAllAvailablePlanets();
}

public interface IProjectileFactory
{
    ISimulatedEntity CreateBullet(Vector2 position);
    CelestialSystem celestialSystem { get; set; }
}