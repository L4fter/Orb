using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var planet = other.gameObject.GetComponent<Planet>();
        if (!planet)
        {
            return;
        }

        planet.ReceiveDamage(damage);
        
        Destroy(this.gameObject);
        GetComponent<GravitatingObject>().DestroyEntity();
        Debug.Log($"Bullet hit {other.gameObject.name}");
    }
}