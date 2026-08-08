using UnityEngine;

namespace FXs.Shooter
{
    public class Projectile : MonoBehaviour
    {
        // Also need to add physics material ...
        public float bounciness = 1f;
        public float mass = 1f;
        public Vector3 velocity;
        public float radius;

        public void Set(Vector3 position, Vector3 force)
        {
            radius = 0.5f * transform.lossyScale.x;
            velocity = force / mass;
            transform.LookAt(position + force, Vector3.up);
            transform.position = position;
        }
    }
}
