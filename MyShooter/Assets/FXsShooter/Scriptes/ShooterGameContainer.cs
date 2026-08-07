using System.Collections.Generic;

namespace FXs.Shooter
{
    public class ShooterGameContainer : MonoGameContainer
    {
        public override List<IGameEntity> AddGameEntities()
        {
            ITrajectoryRenderer trajectoryRenderer = new TrajectoryRenderer();
            IProjectilePhysics projectilePhysics = new ProjectilePhysics();
            IEffectsController effectsController = new EffectsController();
            IProjectileFactory projectileFactory = new ProjectileFactory();

            return new List<IGameEntity>
            {
                trajectoryRenderer,
                projectilePhysics,
                effectsController,
                projectileFactory
            };
        }
    }
}
