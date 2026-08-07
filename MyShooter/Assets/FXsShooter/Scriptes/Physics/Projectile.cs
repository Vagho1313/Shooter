using UnityEngine;

namespace FXs.Shooter
{
    public class Projectile : MonoBehaviour
    {
        public float mass = 1f;
        public Vector3 velocity;
        public void Set(Vector3 position, Vector3 force)
        {
            velocity = force / mass;
            transform.LookAt(position + force, Vector3.up);
            transform.position = position;
        }
    }
}
