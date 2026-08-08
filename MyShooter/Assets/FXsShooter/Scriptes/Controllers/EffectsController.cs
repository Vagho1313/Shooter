using UnityEngine;

namespace FXs.Shooter
{
    public class EffectsController : IEffectsController
    {
        private IProjectilePhysics projectilePhysics;
        private ObjectPool<HitEffect> hitEffectPool;

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
            }
        }

        public void StartGame()
        {
            projectilePhysics.OnProjectileHit += ProjectileHit;
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
        }

        private void ProjectileHit(Vector3 point, Vector3 normal)
        {
            HitEffect hitEffect = hitEffectPool.GetObject();

            hitEffect.Setup(point, normal);

            //Why need to use Renderer Texture???
        }
    }
}

