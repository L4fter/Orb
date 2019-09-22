using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 20;

    public void Launch(Vector2 direction)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var planet = other.gameObject.GetComponent<Planet>();
        if (!planet)
        {
            return;
        }

        planet.ReceiveDamage(damage);
        
        Destroy(this.gameObject);
        Debug.Log($"Bullet hit {other.gameObject.name}");
    }
}