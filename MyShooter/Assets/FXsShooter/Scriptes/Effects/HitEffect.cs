using UnityEngine;

namespace FXs.Shooter
{
    public class HitEffect : MonoBehaviour
    {
        public void Setup(Vector3 point, Vector3 normal)
        {
            transform.position = point + 0.01f * normal;
            transform.LookAt(point - normal);
        }
    }
}
