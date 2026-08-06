using System.Collections.Generic;
using UnityEngine;

namespace FXs
{
    [CreateAssetMenu(fileName = "Game Container", menuName = "FXs/Game Container", order = 0)]
    public class ScriptableGameContainer : ScriptableObject, IGameContainer
    {
        [SerializeField] private Container[] containers;
        [SerializeField] private ScriptableGameEntity[] entities;

        public Container[] GetContainers()
        {
            return containers;
        }

        public IGameEntity[] GetGameEntities()
        {
            List<IGameEntity> gameEntities = new(entities.Length);

            foreach (ScriptableGameEntity entity in entities)
            {
                gameEntities.Add(entity);
            }

            List<IGameEntity> gameNewEntities = AddGameEntities();

            foreach (var gameNewEntity in gameNewEntities)
            {
                gameEntities.Add(gameNewEntity);
            }

            return gameEntities.ToArray();
        }

        public virtual List<IGameEntity> AddGameEntities()
        {
            return new List<IGameEntity>();
        }
    }
}