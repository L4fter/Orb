using System.Collections.Generic;
using System.Linq;

public class CelestialSystem
{
    private Solver solver;
    public List<IPlanet> Planets { get; } = new List<IPlanet>();
    public List<ISimulatedEntity> Projectiles { get; } = new List<ISimulatedEntity>();

    public IPlanet centralStar { get; set; }

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

            if (this.Planets.Contains(planet))
            {
                continue;
            }

            planet.SimulatedEntity.Velocity = OrbitalMath.GetVelocityForCircularOrbitAtRadius(
                planet.SimulatedEntity.Position - centralStar.SimulatedEntity.Position,
                centralStar.SimulatedEntity.Mass);
            
            solver.AddEntity(planet.SimulatedEntity);
            this.Planets.Add(planet);
        }
    }

    public void AddRaw(IEnumerable<IPlanet> planetsToAdd)
    {
        foreach (var planet in planetsToAdd)
        {
            if (centralStar == planet)
            {
                continue;
            }

            if (this.Planets.Contains(planet))
            {
                continue;
            }
           
            solver.AddEntity(planet.SimulatedEntity);
            this.Planets.Add(planet);
        }
    }

    public void AddCentralStar(IPlanet centralStar)
    {
        this.centralStar = centralStar;
        centralStar.SimulatedEntity.IsAttractedByOthers = false;
        solver.AddEntity(centralStar.SimulatedEntity);
    }

    public void AddProjectile(ISimulatedEntity entity)
    {
        Projectiles.Add(entity);
        solver.AddEntity(entity);
        entity.Destroyed += () =>
        {
            Projectiles.Remove(entity);
            solver.RemoveEntity(entity);
        };
    }
}