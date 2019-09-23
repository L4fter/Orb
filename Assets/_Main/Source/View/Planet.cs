using System;
using Core;
using UnityEngine;

public class Planet : MonoBehaviour, IPlanet
{
    public int hp = 100;
    public int startHp = 100;

    public ISimulatedEntity SimulatedEntity => GetComponent<ISimulatedEntity>();
    public int Hp => hp;
    public int StartHp => startHp;
    public PlanetController ControlledBy { get; set; }
    public bool ControlledByPlayer { get; set; }

    public void Start()
    {
        Created?.Invoke(this);
    }

    public void ReceiveDamage(int damage)
    {
        if (hp <= 0)
        {
            return;
        }
        
        hp -= damage;

        if (hp <= 0)
        {
            SetAppearance(new Color(0.1f, 0.1f, 0.1f));
            Debug.Log($"Planet destroyed {gameObject.name}");
        }
    }

    public PlanetAppearance Appearance { get; set; }
    public IWeapon Weapon { get; set; }
    public static event Action<Planet> Created;

    public void SetAppearance(Color color)
    {
        Appearance = new PlanetAppearance
        {
            color = color
        };
        GetComponent<SpriteRenderer>().color = color;
    }

    public void GetAppearanceFromGameObject()
    {
        var color = GetComponent<SpriteRenderer>().color;
        Appearance = new PlanetAppearance
        {
            color = color
        };
    }
}