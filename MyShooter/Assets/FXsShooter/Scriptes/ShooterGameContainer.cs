using System.Collections.Generic;

namespace FXs.Shooter
{
    public class ShooterGameContainer : MonoGameContainer
    {
        public override List<IGameEntity> AddGameEntities()
        {
            ILevelController levelController = new LevelController();
            IProjectilePhysics projectilePhysics = new ProjectilePhysics();
            IEffectsController effectsController = new EffectsController();
            IProjectileFactory projectileFactory = new ProjectileFactory();

            return new List<IGameEntity>
            {
                levelController,
                projectilePhysics,
                effectsController,
                projectileFactory
            };
        }
    }
}
