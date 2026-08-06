using System.Collections.Generic;

namespace FXs.Shooter
{
    public class ShooterGameContainer : MonoGameContainer
    {
        public override List<IGameEntity> AddGameEntities()
        {
            TrajectoryRenderer trajectoryRenderer = new TrajectoryRenderer();
            ProjectilePhysics projectilePhysics = new ProjectilePhysics();
            EffectsController effectsController = new EffectsController();

            return new List<IGameEntity>
            {
                trajectoryRenderer,
                projectilePhysics,
                effectsController
            };
        }
    }
}
