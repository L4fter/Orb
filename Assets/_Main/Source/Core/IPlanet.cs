public interface IPlanet
{
    ISimulatedEntity SimulatedEntity { get; }
    int Hp { get; }
    int StartHp { get; }
    bool IsControlled { get; }
    PlanetController ControlledBy { get; set; }
    bool ControlledByPlayer { get; set; }
    void ReceiveDamage(int damage);
}