using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Meta.PoorMansDi;
using UnityEngine;

public class ProjectileFactory : DiMonoBehaviour, IProjectileFactory
{
    public GameObject bulletPrefab;
    
    [UsedImplicitly]
    public void Init(IBinder binder)
    {
        binder.Bind<IProjectileFactory>().ToSingle(this);
    }

    public ISimulatedEntity CreateBullet(Vector2 position)
    {
        var bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        var gravitatingObject = bullet.GetComponent<GravitatingObject>();
        celestialSystem.Add(gravitatingObject);
        gravitatingObject.Position = position;
        return gravitatingObject;
    }

    public CelestialSystem celestialSystem { get; set; }
}
