using System.Linq;
using Meta.PoorMansDi;
using UnityEngine;

public class Game
{
    private IWinLoseHandler winLoseHandler;
    private readonly IResolver resolver;
    private readonly InputPlanetController inputPlanetController;
    private readonly AiPlanetController aiPlanetController;
    private CelestialSystem celestialSystem;
    private IPlanetFactory planetFactory;
    private IProjectileFactory projectileFactory;

    public Game(IWinLoseHandler winLoseHandler, IResolver resolver, InputPlanetController inputPlanetController, AiPlanetController aiPlanetController)
    {
        this.winLoseHandler = winLoseHandler;
        this.resolver = resolver;
        this.inputPlanetController = inputPlanetController;
        this.aiPlanetController = aiPlanetController;
    }

    public void MakeTimestep(float deltaTime)
    {
        celestialSystem.SimulateTimestep(deltaTime);

        if (celestialSystem.IsPlayerAlive &&
            !celestialSystem.GetAliveAiPlanets().Any())
        {
            //winLoseHandler.Win();
            return;
        }

        if (!celestialSystem.IsPlayerAlive)
        {
            winLoseHandler.Lose();
        }
        
    }

    public void OnViewReady()
    {
        Debug.Log("View ready");
        
        // Injecting stuff that couldnt be created before the game
        // Mostly from scene, which is loaded later at unknown time
        planetFactory = resolver.Resolve<IPlanetFactory>();
        projectileFactory = resolver.Resolve<IProjectileFactory>();

        if (true)
        {
            CreateCelestialSystemFromScene();
        }
        
        projectileFactory.celestialSystem = celestialSystem;
    }

    private void CreateCelestialSystemFromScene()
    {
        celestialSystem = resolver.Resolve<CelestialSystem>();
        var planets = planetFactory.CollectAllAvailablePlanets();
        var centralStar = planets.OrderByDescending(planet => planet.SimulatedEntity.Mass).First();
        
        celestialSystem.AddCentralStar(centralStar);
        celestialSystem.Add(planets);

        var onlyPlanets = planets.Except(new[] {centralStar}).ToList();
        var randomIndex = Random.Range(0, onlyPlanets.Count);
        var randomPlanet = onlyPlanets.Skip(randomIndex).First();
        inputPlanetController.Control(randomPlanet, projectileFactory);

        onlyPlanets.Remove(randomPlanet);
        
        randomIndex = Random.Range(0, onlyPlanets.Count);
        randomPlanet = onlyPlanets.Skip(randomIndex).First();
        aiPlanetController.Control(randomPlanet, projectileFactory);
        
    }

    public void Discard()
    {
        
    }

    public void WaitForViewReady()
    {
        // just chill
    }
}

public class PlanetController
{
    protected IPlanet planet;
    protected IProjectileFactory factory;
    protected void ShootAtDirection(Vector2 direction)
    {
        Vector2 pos2d = planet.SimulatedEntity.Position;
        var distanceFromCenter = 1f;
        var bullet = factory.CreateBullet(pos2d + direction * distanceFromCenter);
        bullet.Velocity = planet.SimulatedEntity.Velocity;
        bullet.Acceleration = direction * 4;
        bullet.TimeForAcceleration = 0.2f;
    }
}

public class AiPlanetController : PlanetController, IUpdatesReceiver
{
    private float lastShootTime = Time.time;
    
    public void Update()
    {
        if (lastShootTime + 1 > Time.time)
        {
            return;
        }
        
        ShootAtDirection(Random.insideUnitCircle);
        lastShootTime = Time.time;
    }

    public void Control(IPlanet randomPlanet, IProjectileFactory projectileFactory)
    {
        factory = projectileFactory;
        this.planet = randomPlanet;
    }
}

public interface IUpdatesReceiver
{
    void Update();
}

public class InputPlanetController : PlanetController, IInputReceiver
{
    public void Fire(Vector2 target)
    {
        var pos2d = planet.SimulatedEntity.Position;
        var direction = (target - pos2d).normalized;
        Debug.DrawLine(pos2d, pos2d + direction, Color.red);
        
        ShootAtDirection(direction);
    }

    public void Control(IPlanet randomPlanet, IProjectileFactory projectileFactory)
    {
        factory = projectileFactory;
        this.planet = randomPlanet;
        planet.ControlledByPlayer = true;
    }
}