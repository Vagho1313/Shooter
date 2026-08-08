using System.Threading.Tasks;
using UnityEngine;

namespace FXs.Shooter
{
    public class EffectsController : IEffectsController
    {
        private IProjectilePhysics projectilePhysics;
        private ObjectPool<HitEffect> hitEffectPool;
        private ObjectPool<ParticleSystem> projectileDestroyPool;

        public void Init()
        {

        }

        public void Setup(GameContext context)
        {
            if (context.TryGetEntity(out IProjectilePhysics projectilePhysics))
            {
                this.projectilePhysics = projectilePhysics;
            }

            if (context.TryGetContainer(out ShooterGameSettings settings))
            {
                hitEffectPool = settings.CreateHitEffectPool();
                projectileDestroyPool = settings.CreateProjectilDestroyPool();
            }
        }

        public void StartGame()
        {
            projectilePhysics.OnProjectileHit += ProjectileHit;
            projectilePhysics.OnProjectileDestroyed += ProjectileDestroyed;
        }

        public void UpdateGame()
        {

        }

        public void FixedUpdateGame()
        {

        }

        public void EndGame()
        {
            projectilePhysics.OnProjectileHit -= ProjectileHit;
            projectilePhysics.OnProjectileDestroyed -= ProjectileDestroyed;
        }

        private void ProjectileHit(Vector3 point, Vector3 normal)
        {
            HitEffect hitEffect = hitEffectPool.GetObject();

            hitEffect.Setup(point, normal);

            //Why need to use Renderer Texture???
        }

        private void ProjectileDestroyed(Projectile projectile)
        {
            ParticleSystem particleSystem = projectileDestroyPool.GetObject();

            particleSystem.transform.position = projectile.transform.position;

            particleSystem.Play();

            _ = DestroyWhenStopped(particleSystem);
        }

        private async Task DestroyWhenStopped(ParticleSystem particleSystem)
        {
            while (particleSystem != null && particleSystem.isPlaying)
            {
                await Task.Yield();
            }

            if (particleSystem != null)
            {
                projectileDestroyPool.HideObject(particleSystem);
            }
        }
    }
}

