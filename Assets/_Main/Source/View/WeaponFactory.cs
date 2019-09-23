using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Meta.PoorMansDi;

public class WeaponFactory : IWeaponFactory
{
    private readonly Dictionary<WeaponType, ISingleWeaponFactory> factoriesForEachWeapon = new Dictionary<WeaponType, ISingleWeaponFactory>();

    public WeaponFactory(IResolver resolver)
    {
        var singleFactories = 
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(ISingleWeaponFactory).IsAssignableFrom(type))
                .Where(type => type.IsClass)
                .Where(type => !type.IsAbstract)
                .Select(type => Activator.CreateInstance(type, resolver))
                .Cast<ISingleWeaponFactory>();

        foreach (var factory in singleFactories)
        {
            RegisterWeapon(factory.Type, factory);
        }
    }

    public List<WeaponType> GetAllAvailableTypes()
    {
        return factoriesForEachWeapon.Keys.ToList();
    }

    private void RegisterWeapon(WeaponType type, ISingleWeaponFactory factory)
    {
        factoriesForEachWeapon.Add(type, factory);
    }

    public IWeapon AddWeapon(WeaponType type, IPlanet planet)
    {
        return factoriesForEachWeapon[type].FactoryFunction(planet);
    }

    public IWeapon AddWeapon(string typeName, IPlanet planet)
    {
        if (string.IsNullOrEmpty(typeName))
        {
            return null;
        }
        return factoriesForEachWeapon.First(pair => pair.Key.Name == typeName).Value.FactoryFunction(planet);
    }
}