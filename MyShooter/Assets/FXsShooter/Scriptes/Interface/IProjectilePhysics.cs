using System;
using UnityEngine;

namespace FXs.Shooter
{
    public interface IProjectilePhysics : IGameEntity
    {
        event Action<int, Vector3, Vector3> OnShot;
        event Action<Vector3, Vector3> OnProjectileHit;
        event Action<Projectile> OnProjectileDestroyed;
    }
}
