using UnityEngine;

namespace FXs.Shooter
{
    public class HitEffect : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private Vector3 size;

        private Mesh mesh;

        public void Setup(
            Vector3 point,
            Vector3 normal,
            Vector2 wallSize,
            Transform wall)
        {
            size = transform.lossyScale;

            transform.SetPositionAndRotation(point - normal * 0.001f, Quaternion.LookRotation(-normal));

            Vector3 localHit = point - wall.transform.position;

            localHit = new Vector3(Vector3.Dot(localHit, wall.transform.right), Vector3.Dot(localHit, wall.transform.up), 0f);

            float deltaX = 0f;
            float deltaY = 0f;

            float uMin = 0f;
            float uMax = 1f;
            float vMin = 0f;
            float vMax = 1f;

            if (localHit.x + 0.5f * size.x > 0.5f * wallSize.x)
            {
                deltaX = localHit.x + 0.5f * size.x - 0.5f * wallSize.x;
                uMax = 1f - deltaX / size.x;
            }
            else if (localHit.x - 0.5f * size.x < -0.5f * wallSize.x)
            {
                deltaX = localHit.x - 0.5f * size.x + 0.5f * wallSize.x;
                uMin = -deltaX / size.x;
            }

            if (localHit.y + 0.5f * size.y > 0.5f * wallSize.y)
            {
                deltaY = localHit.y + 0.5f * size.y - 0.5f * wallSize.y;
                vMax = 1f - deltaY / size.y;
            }
            else if (localHit.y - 0.5f * size.y < -0.5f * wallSize.y)
            {
                deltaY = localHit.y - 0.5f * size.y + 0.5f * wallSize.y;
                vMin = -deltaY / size.y;
            }

            transform.localScale = new Vector3(size.x - Mathf.Abs(deltaX), size.y - Mathf.Abs(deltaY), size.z);

            transform.position -= 0.5f * deltaX * wall.right + 0.5f * deltaY * wall.up;

            ApplyUV(uMin, uMax, vMin, vMax);
        }

        private void ApplyUV(float uMin, float uMax, float vMin, float vMax)
        {
            if (mesh == null)
            {
                mesh = Instantiate(meshFilter.sharedMesh);
                meshFilter.mesh = mesh;
            }

            Vector2[] uv = mesh.uv;

            if (uv.Length < 4)
            {
                Debug.LogWarning("HitEffect mesh must contain UV.");
                return;
            }

            uv[0] = new Vector2(uMin, vMin);
            uv[1] = new Vector2(uMax, vMin);
            uv[2] = new Vector2(uMin, vMax);
            uv[3] = new Vector2(uMax, vMax);

            mesh.uv = uv;
        }
    }
}