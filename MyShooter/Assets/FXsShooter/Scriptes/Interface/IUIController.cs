using System;

namespace FXs.Shooter
{
    public interface IUIController : IGameEntity
    {
        event Action<int> OnPowerChanged;

        public int ShotPower { get; }
    }
}
