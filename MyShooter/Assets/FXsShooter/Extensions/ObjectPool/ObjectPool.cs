using System.Collections.Generic;
using UnityEngine;

namespace FXs
{
    public class ObjectPool<Object> where Object : Component
    {
        private readonly Object prefab;

        private readonly List<Object> inactiveObjects;

        private readonly List<Object> activeObjects;

        public ObjectPool(Object prefab)
        {
            this.prefab = prefab;

            inactiveObjects = new();

            activeObjects = new();
        }

        public Object GetObject()
        {
            if (inactiveObjects.Count > 0)
            {
                Object obj = inactiveObjects[0];
                inactiveObjects.RemoveAt(0);

                activeObjects.Add(obj);

                obj.gameObject.SetActive(true);

                return obj;
            }

            Object newObject = Component.Instantiate(prefab);
            activeObjects.Add(newObject);

            newObject.gameObject.SetActive(true);

            return newObject;
        }

        public void HideAllObjects()
        {
            while(activeObjects.Count > 0)
            {
                Object obj = activeObjects[0];
                activeObjects.RemoveAt(0);

                inactiveObjects.Add(obj);

                obj.gameObject.SetActive(false);
            }
        }

        public bool HideObject(Object obj)
        {
            if (activeObjects.Contains(obj))
            {
                activeObjects.Remove(obj);

                inactiveObjects.Add(obj);

                obj.gameObject.SetActive(false);

                return true;
            }
            return false;
        }
    }
}