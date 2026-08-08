using UnityEngine;

namespace FXs.Shooter
{
    public class Plane : MonoBehaviour
    {
        // Also need to add physics material ...
        public float bounciness = 1f;
        public Vector3 Position => transform.position;
        public Vector3 Normal => -transform.forward;
        public Vector2 Size => new Vector2(transform.lossyScale.x, transform.lossyScale.y);
    }
}
