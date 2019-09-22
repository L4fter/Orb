using System.Linq;
using Meta.PoorMansDi;
using UnityEngine;

public class PlanetFactory : DiMonoBehaviour, IPlanetFactory
{
    public Planet planetPrefab;
    
    public void Init(IBinder binder)
    {
        binder.Bind<IPlanetFactory>().ToSingle(this);
    }

    public IPlanet CreatePlanet(Vector2 Position)
    {
        var planet = Instantiate(planetPrefab, Position, Quaternion.identity);
        return planet;
    }

    public IPlanet[] CollectAllAvailablePlanets()
    {
        var allPlanets = FindObjectsOfType<Planet>();
        return allPlanets.Cast<IPlanet>().ToArray();
    }
}
