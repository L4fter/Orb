using System.Threading.Tasks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;
    private IPlanet ownerPlanet;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var planet = other.gameObject.GetComponent<Planet>();
        if (!planet)
        {
            return;
        }

        if (planet != ownerPlanet)
        {
            planet.ReceiveDamage(damage);
        }
        
        Destroy(this.gameObject);
        GetComponent<GravitatingObject>().DestroyEntity();
    }

    public async void SetOwnership(IPlanet planet, float delay)
    {
        ownerPlanet = planet;
        this.GetComponent<TrailRenderer>().startColor = planet.Appearance.color;
    }
}