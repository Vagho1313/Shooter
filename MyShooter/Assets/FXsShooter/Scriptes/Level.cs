using UnityEngine;

namespace FXs.Shooter
{
    public class Level : MonoBehaviour
    {
        public Plane[] Planes { get; private set; }

        public void Setup()
        {
            Planes = GetComponentsInChildren<Plane>();
        }
    }
}
