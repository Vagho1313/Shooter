using System;

namespace FXs.Shooter
{
    public interface IProjectilePhysics : IGameEntity
    {
        event Action<int> OnShot;
    }
}
