using System;
using UnityEngine;

public class PlanetHpCreator : MonoBehaviour
{
    public PlanetHpBar planetHpBarPrefab;
    public Transform canvas;
    
    public void Awake()
    {
        Planet.Created += OnPlanetCreated;
    }

    private void OnDestroy()
    {
        Planet.Created -= OnPlanetCreated;
    }

    private void OnPlanetCreated(Planet planet)
    {
        var planetHpBar = Instantiate(planetHpBarPrefab);
        planetHpBar.transform.SetParent(canvas);
        planetHpBar.Init(planet);
    }
}