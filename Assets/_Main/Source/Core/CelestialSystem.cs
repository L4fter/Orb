using System.Collections.Generic;
using System.Linq;

internal class CelestialSystem
{
    private Solver solver;
    private IPlanet[] planets;

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
    }
}