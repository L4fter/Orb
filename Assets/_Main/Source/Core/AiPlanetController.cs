using UnityEngine;

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
}