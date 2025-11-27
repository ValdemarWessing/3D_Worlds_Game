// csharp
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshFilter))]
public class meshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    [Header("Noise")]
    [Range(0.01f, 2f)]
    public float frequency = 0.3f;
    [Range(0f, 1000f)]
    public float amplitude = 2f;
    private float noiseOffsetX;
    private float noiseOffsetZ;

    // Rebuild in editor when values change
    void OnValidate()
    {
        // ensure mesh/filter exist so editor changes update immediately
        if (mesh == null)
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }

        // avoid running during domain reloads with invalid sizes
        if (xSize < 1) xSize = 1;
        if (zSize < 1) zSize = 1;

        CreateShape();
        UpdateMesh();
    }

    void Start()
    {
        noiseOffsetX = Random.Range(0f, 1000f);
        noiseOffsetZ = Random.Range(0f, 1000f);
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise((x + noiseOffsetX) * frequency, (z + noiseOffsetZ) * frequency) * amplitude;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        
        MeshCollider collider = GetComponent<MeshCollider>();
        if (collider == null)
            collider = gameObject.AddComponent<MeshCollider>();
        collider.sharedMesh = mesh;
    }
}