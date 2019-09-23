using Core;
using UnityEngine;

public interface IPlanet
{
    ISimulatedEntity SimulatedEntity { get; }
    int Hp { get; }
    int StartHp { get; }
    PlanetController ControlledBy { get; set; }
    bool ControlledByPlayer { get; set; }
    void ReceiveDamage(int damage);    
    PlanetAppearance Appearance { get; set; }
    IWeapon Weapon { get; set; }
}

public struct PlanetAppearance
{
    public Color color;
}
