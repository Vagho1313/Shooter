using System.Collections.Generic;
using UnityEngine;

namespace FXs.Shooter
{
    public class ProjectileFactory : IProjectileFactory
    {
        private ObjectPool<Projectile> projectilePool;
        private IProjectilePhysics projectilePhysics;
        private ICannonController cannonController;

        public List<Projectile> Projectiles { get; private set; }

        public void Init()
        {
            
        }

        public void Setup(GameContext context)
        {
            if (context.TryGetContainer(out ShooterGameSettings settings))
            {
                projectilePool = settings.CreateProjectilePool();
            }

            if (context.TryGetEntity(out IProjectilePhysics projectilePhysics))
            {
                this.projectilePhysics = projectilePhysics;
            }

            if (context.TryGetEntity(out ICannonController cannonController))
            {
                this.cannonController = cannonController;
            }
        }

        public void StartGame()
        {
            Projectiles = new List<Projectile>();
            projectilePhysics.OnShot += ShotProjectile;
            projectilePhysics.OnProjectileDestroyed += RemoveProjectile;
        }

        public void UpdateGame()
        {
            
        }

        public void FixedUpdateGame()
        {
            
        }

        public void EndGame()
        {
            projectilePhysics.OnShot -= ShotProjectile;
            projectilePhysics.OnProjectileDestroyed -= RemoveProjectile;

            if (Projectiles != null)
            {
                foreach (var item in Projectiles)
                {
                    Object.Destroy(item.gameObject);
                }
            }
            Projectiles = null;
        }

        private void RemoveProjectile(Projectile projectile)
        {
            if (projectilePool.HideObject(projectile))
            {
                Projectiles.Remove(projectile);
            }
        }

        private void ShotProjectile(int power, Vector3 position, Vector3 direction)
        {
            if(!cannonController.CanShot)
            {
                return;
            }    
            Projectile projectile = projectilePool.GetObject();
            if(!Projectiles.Contains(projectile))
            {
                projectile.Set(position, power * direction);
                Projectiles.Add(projectile);
            }
        }
    }
}
