using System;
using System.Collections;
using Core;
using JetBrains.Annotations;
using Meta.PoorMansDi;
using UnityEngine;

public class MultiShotWeapon : MonoBehaviour, IWeapon
{
    private IPlanet planet;
    private IProjectileFactory projectileFactory;
    private bool isShooting;
    public float ReloadTime => 3f; 
    public float ReloadAmount { get; private set; }
    
    public void Init(IPlanet planet, IProjectileFactory projectileFactory)
    {
        this.planet = planet;
        this.projectileFactory = projectileFactory;
    }

    private void Update()
    {
        if (ReloadAmount < 1 && !isShooting)
        {
            ReloadAmount = Mathf.Clamp01(ReloadAmount + Time.deltaTime / ReloadTime);
        }
    }

    public void Shoot(Vector2 direction)
    {
        if (ReloadAmount < 1)
        {
            return;
        }

        ShootImpl(direction.normalized);
    }

    public WeaponType Type =>  new WeaponType(){Name = "MultiShot"};
    
    private void ShootImpl(Vector2 direction)
    {
        StartCoroutine(ShootMultiple(direction));
    }

    private IEnumerator ShootMultiple(Vector2 direction)
    {
        isShooting = true;

        for (int i = 0; i < 5; i++)
        {
            ShootSingle(direction);
            yield return new WaitForSeconds(1/20f);
        }
        
        isShooting = false;
    }
    
    private void ShootSingle(Vector2 direction)
    {
        Vector2 pos2d = planet.SimulatedEntity.Position;
        var distanceFromCenter = 0.4f;
        var bulletEntity = projectileFactory.CreateBullet(pos2d + direction * distanceFromCenter) as GravitatingObject;
        
        bulletEntity.Velocity = planet.SimulatedEntity.Velocity;
        bulletEntity.Acceleration = direction * 3;
        bulletEntity.TimeForAcceleration = 0.3f;

        var bullet = bulletEntity.GetComponent<Bullet>();
        bullet.SetOwnership(planet, 1f);
        bullet.damage = 5;
        bullet.transform.localScale *= 0.5f;

        ReloadAmount = 0;
    }
}

[UsedImplicitly]
public class MultiShotWeaponFactory : ISingleWeaponFactory
{
    private readonly IResolver resolver;

    public MultiShotWeaponFactory(IResolver resolver)
    {
        this.resolver = resolver;
    }

    public WeaponType Type =>  new WeaponType(){Name = "MultiShot"};
    
    public Func<IPlanet, IWeapon> FactoryFunction => FactoryFunc;

    private IWeapon FactoryFunc(IPlanet planet)
    {
        var planetView = (Planet) planet;
        var weapon = planetView.gameObject.AddComponent<MultiShotWeapon>();
        planet.Weapon = weapon;
        weapon.Init(planet, resolver.Resolve<IProjectileFactory>());
        return weapon;
    }

}