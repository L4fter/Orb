public interface IPlanet
{
    ISimulatedEntity SimulatedEntity { get; }
    float Hp { get; }
    bool IsControlled { get; }
    ICharacter ControlledBy { get; }
    bool ControlledByPlayer { get; set; }
    void ReceiveDamage(int damage);
}