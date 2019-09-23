namespace Core
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IWeapon
    {
        float ReloadTime { get; }
        float ReloadAmount { get; }
        void Shoot(Vector2 direction);
        WeaponType Type { get; }
    }

    public interface ISingleWeaponFactory
    {
        WeaponType Type { get; }

        Func<IPlanet, IWeapon> FactoryFunction { get; }
    }

    public interface IWeaponFactory
    {
        List<WeaponType> GetAllAvailableTypes();
        IWeapon AddWeapon(WeaponType type, IPlanet planet);
        IWeapon AddWeapon(string typeName, IPlanet planet);
    }
}