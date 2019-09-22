using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Planet : MonoBehaviour, IPlanet
{
    public int startHp = 100;
    public int hp = 100;

    public ISimulatedEntity SimulatedEntity => GetComponent<ISimulatedEntity>();
    public int Hp => hp;
    public int StartHp => startHp;
    public bool IsControlled { get; }
    public PlanetController ControlledBy { get; set; }
    public bool ControlledByPlayer { get; set; }

    public void ReceiveDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
//            Destroy(this.gameObject);
            Debug.Log($"Planet destroyed {gameObject.name}");
        }
    }
}
