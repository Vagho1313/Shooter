using UnityEngine;

namespace FXs
{
    public abstract class ScriptableGameEntity : ScriptableObject, IGameEntity
    {
        public abstract void Init();

        public abstract void Setup(GameContext context);

        public abstract void StartGame();

        public abstract void UpdateGame();

        public abstract void FixedUpdateGame();

        public abstract void EndGame();
    }
}
