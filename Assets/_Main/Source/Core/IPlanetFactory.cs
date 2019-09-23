using UnityEngine;

public interface IPlanetFactory
{
    IPlanet CreatePlanet(Vector2 position, PlanetAppearance? appearance = default);

    IPlanet[] CollectAllAvailablePlanets();
}

public interface IProjectileFactory
{
    ISimulatedEntity CreateBullet(Vector2 position);
    CelestialSystem celestialSystem { get; set; }
}