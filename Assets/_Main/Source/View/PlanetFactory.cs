using System.Linq;
using JetBrains.Annotations;
using Meta.PoorMansDi;
using UnityEngine;

public class PlanetFactory : DiMonoBehaviour, IPlanetFactory
{
    public Planet planetPrefab;
    
    [UsedImplicitly]
    public void Init(IBinder binder)
    {
        binder.Bind<IPlanetFactory>().ToSingle(this);
    }

    public IPlanet CreatePlanet(Vector2 position)
    {
        var planet = Instantiate(planetPrefab, position, Quaternion.identity);
        planet.SimulatedEntity.Position = position;
        return planet;
    }

    public IPlanet[] CollectAllAvailablePlanets()
    {
        var allPlanets = FindObjectsOfType<Planet>();
        foreach (var allPlanet in allPlanets)
        {
            allPlanet.SimulatedEntity.Position = allPlanet.transform.position;
        }
        return allPlanets.Cast<IPlanet>().ToArray();
    }
}
