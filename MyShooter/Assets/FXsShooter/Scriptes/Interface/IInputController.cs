using System;
using UnityEngine;

namespace FXs.Shooter
{
    public interface IInputController : IGameEntity
    {
        event Action<float, Vector2> OnAim;
        event Action OnShoot;
    }
}
