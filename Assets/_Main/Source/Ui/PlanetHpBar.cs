using System;
using UnityEngine;
using UnityEngine.UI;

public class PlanetHpBar : MonoBehaviour
{
    public Image fill;
    private IPlanet planet;

    public void Init(IPlanet planet)
    {
        this.planet = planet;
    }

    private void Update()
    {
        this.transform.position = planet.SimulatedEntity.Position + new Vector2(0, -0.2f);
        fill.fillAmount = 1 - planet.Hp/(float)planet.StartHp;
    }
}