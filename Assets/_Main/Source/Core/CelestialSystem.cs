using System.Collections.Generic;
using System.Linq;

public class CelestialSystem
{
    private Solver solver;
    private List<IPlanet> planets = new List<IPlanet>();
    private IPlanet centralStar;

    public CelestialSystem(Solver solver)
    {
        this.solver = solver;
    }

    public void SimulateTimestep(float dT)
    {
        solver.SimulateTimeStep(dT);
    }

    public bool IsPlayerAlive => true;

    public IPlanet[] GetAliveAiPlanets()
    {
        return new IPlanet[0];
    }

    public void Add(IEnumerable<IPlanet> planetsToAdd)
    {
        foreach (var planet in planetsToAdd)
        {
            if (centralStar == planet)
            {
                continue;
            }

            if (this.planets.Contains(planet))
            {
                continue;
            }

            planet.SimulatedEntity.Velocity = OrbitalMath.GetVelocityForCircularOrbitAtRadius(
                planet.SimulatedEntity.Position - centralStar.SimulatedEntity.Position,
                centralStar.SimulatedEntity.Mass);
            
            solver.AddEntity(planet.SimulatedEntity);
            this.planets.Add(planet);
        }
    }

    public void AddCentralStar(IPlanet centralStar)
    {
        this.centralStar = centralStar;
        centralStar.SimulatedEntity.IsAttractedByOthers = false;
        solver.AddEntity(centralStar.SimulatedEntity);
    }

    public void Add(ISimulatedEntity entity)
    {
        solver.AddEntity(entity);
    }
}