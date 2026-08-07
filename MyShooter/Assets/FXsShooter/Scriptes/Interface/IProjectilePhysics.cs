using System;
using UnityEngine;

namespace FXs.Shooter
{
    public interface IProjectilePhysics : IGameEntity
    {
        event Action<int, Vector3, Vector3> OnShot;
        event Action<Projectile> OnProjectileDestroyed;
    }
}
