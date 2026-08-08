using UnityEngine;

namespace FXs.Shooter
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private float meshRandom = 0.2f;

        // Also need to add physics material ...
        public float bounciness = 1f;
        public float mass = 1f;
        public Vector3 velocity;
        public float radius;

        public void Set(Vector3 position, Vector3 force)
        {
            GenerateMesh();

            radius = 0.5f * transform.lossyScale.x;
            velocity = force / mass;
            transform.LookAt(position + force, Vector3.up);
            transform.position = position;
        }

        private void GenerateMesh()
        {
            Mesh mesh = new Mesh();

            Vector3[] vertices =
            {
            RandomVertex(new Vector3(-0.5f, -0.5f, -0.5f)),
            RandomVertex(new Vector3( 0.5f, -0.5f, -0.5f)),
            RandomVertex(new Vector3( 0.5f,  0.5f, -0.5f)),
            RandomVertex(new Vector3(-0.5f,  0.5f, -0.5f)),

            RandomVertex(new Vector3(-0.5f, -0.5f,  0.5f)),
            RandomVertex(new Vector3( 0.5f, -0.5f,  0.5f)),
            RandomVertex(new Vector3( 0.5f,  0.5f,  0.5f)),
            RandomVertex(new Vector3(-0.5f,  0.5f,  0.5f))
            };

            int[] triangles =
            {
            // Back
            0, 2, 1,
            0, 3, 2,

            // Front
            4, 5, 6,
            4, 6, 7,

            // Left
            0, 4, 7,
            0, 7, 3,

            // Right
            1, 2, 6,
            1, 6, 5,

            // Top
            3, 7, 6,
            3, 6, 2,

            // Bottom
            0, 1, 5,
            0, 5, 4
        };

            mesh.vertices = vertices;
            mesh.triangles = triangles;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            meshFilter.mesh = mesh;
        }

        private Vector3 RandomVertex(Vector3 vertex)
        {
            vertex.x += Random.Range(-meshRandom, meshRandom);
            vertex.y += Random.Range(-meshRandom, meshRandom);
            vertex.z += Random.Range(-meshRandom, meshRandom);

            return vertex;
        }
    }
}
