using System.Collections.Generic;
using System.Linq;

public class CelestialSystem
{
    private Solver solver;
    private IPlanet[] planets;

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

    public void Add(IEnumerable<IPlanet> planets)
    {
        this.planets = planets.ToArray();
        foreach (var planet in this.planets)
        {
            solver.AddEntity(planet.SimulatedEntity);
        }
    }
}