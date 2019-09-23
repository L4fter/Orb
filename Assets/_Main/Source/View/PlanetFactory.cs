using System.Linq;
using JetBrains.Annotations;
using Meta.PoorMansDi;
using UnityEngine;

public class PlanetFactory : DiMonoBehaviour, IPlanetFactory
{
    private Color[] randomPlanetColors = new[] {Color.blue, Color.cyan, Color.green};
    
    public Planet planetPrefab;
    
    [UsedImplicitly]
    public void Init(IBinder binder)
    {
        binder.Bind<IPlanetFactory>().ToSingle(this);
    }

    public IPlanet CreatePlanet(Vector2 position, PlanetAppearance? appearance = default)
    {
        var planet = Instantiate(planetPrefab, position, Quaternion.identity);
        planet.SimulatedEntity.Position = position;
        
        if (appearance is null)
        {
            planet.SetAppearance(randomPlanetColors[Random.Range(0, randomPlanetColors.Length)]);
        }
        else
        {
            planet.SetAppearance(appearance.Value.color);
        }
        return planet;
    }

    public IPlanet[] CollectAllAvailablePlanets()
    {
        var allPlanets = FindObjectsOfType<Planet>();
        foreach (var planet in allPlanets)
        {
            planet.GetAppearanceFromGameObject();
            planet.SimulatedEntity.Position = planet.transform.position;
        }
        return allPlanets.Cast<IPlanet>().ToArray();
    }
}
