using System;
using UnityEngine;

namespace FXs.Shooter
{
    public interface IProjectilePhysics : IGameEntity
    {
        event Action<int, Vector3, Vector3> OnShot;
        event Action<Vector3, Vector3, Vector2, Transform> OnProjectileHit;
        event Action<Projectile> OnProjectileDestroyed;
    }
}
