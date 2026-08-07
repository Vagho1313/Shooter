using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FXs.Shooter
{
    public interface IProjectileFactory : IGameEntity
    {
        public List<Projectile> Projectiles { get; }
    }
}
