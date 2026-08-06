using System;
using System.Collections.Generic;
using UnityEngine;

namespace FXs
{
    public class GameContext
    {
        private readonly Dictionary<Type, Container> _containers;
        private readonly Dictionary<Type, IGameEntity> _entities;

        public GameContext(Container[] containers, IGameEntity[] entities)
        {
            _containers = new();
            _entities = new();

            if (containers != null)
            {
                foreach (var container in containers)
                {
                    if (!_containers.TryAdd(container.GetType(), container))
                    {
                        Debug.LogWarning(container.GetType() + " already exist");
                    }
                }
            }

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    if (!_entities.TryAdd(entity.GetType(), entity))
                    {
                        Debug.LogWarning(entity.GetType() + " already exist");
                    }
                }
            }
        }

        public bool TryGetContainer<C>(out C container) where C : Container
        {
            if (_containers.TryGetValue(typeof(C), out Container value) && value is C c)
            {
                container = c;
                return true;
            }

            container = null;
            return false;
        }

        public bool TryGetEntity<E>(out E entity) where E : IGameEntity
        {
            if (_entities.TryGetValue(typeof(E), out IGameEntity value) && value is E e)
            {
                entity = e;
                return true;
            }

            entity = default;
            return false;
        }
    }
}
