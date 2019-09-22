using UnityEngine;

public interface IWinLoseHandler
{
    void Win();
    void Lose();
}


public interface IPlanet
{
    ISimulatedEntity SimulatedEntity { get; }
    float Hp { get; }
    bool IsControlled { get; }
    ICharacter ControlledBy { get; }
    void ReceiveDamage(int damage);
}

public interface ICharacter
{
    
}

public interface IPlanetFactory
{
    IPlanet CreatePlanet(Vector2 position);

    IPlanet[] CollectAllAvailablePlanets();
}
