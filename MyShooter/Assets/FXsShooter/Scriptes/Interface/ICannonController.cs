using UnityEngine;

namespace FXs.Shooter
{
    public interface ICannonController : IGameEntity
    {
        public Vector3 Position { get; }
        public Vector3 Direction { get; }
        public bool CanShot { get; }
    }
}
