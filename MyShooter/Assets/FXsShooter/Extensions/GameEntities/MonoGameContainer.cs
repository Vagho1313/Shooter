using System.Collections.Generic;
using UnityEngine;

namespace FXs
{
    public class MonoGameContainer : MonoBehaviour, IGameContainer
    {
        [SerializeField] private Container[] containers;
        [SerializeField] private MonoGameEntity[] entities;

        public Container[] GetContainers()
        {
            return containers;
        }

        public IGameEntity[] GetGameEntities()
        {
            List<IGameEntity> gameEntities = new(entities.Length);

            foreach (MonoGameEntity entity in entities)
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
