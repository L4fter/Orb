using UnityEngine;

internal class Planet : MonoBehaviour
{
    public int startHp = 100;
    public int hp = 100;
    
    public void ReceiveDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log($"Planet destroyed {gameObject.name}");
        }
    }
}