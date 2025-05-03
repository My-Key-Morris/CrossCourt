using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CircularRoom : MonoBehaviour
{
    public float radius = 5f;
    public float wallThickness = 0.2f;
    public float height = 2f;
    public int segments = 32;

    private float lastRadius = -1f;
    private float lastWallThickness = -1f;
    private float lastHeight = -1f;
    private int lastSegments = -1;

    void OnValidate()
    {
        if (radius != lastRadius || wallThickness != lastWallThickness || height != lastHeight || segments != lastSegments)
        {
            GenerateRoom();
            lastRadius = radius;
            lastWallThickness = wallThickness;
            lastHeight = height;
            lastSegments = segments;
        }
    }

    void Start()
    {
        GenerateRoom();
    }

    void GenerateRoom()
    {
        Mesh wallMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = wallMesh;

        float innerRadius = radius - wallThickness;
        Vector3[] vertices = new Vector3[segments * 4];
        Vector2[] uvs = new Vector2[segments * 4];
        Vector3[] normals = new Vector3[segments * 4]; // Add normals array
        int[] triangles = new int[segments * 24];

        // Define Vertices, UVs, and Normals
        for (int i = 0; i < segments; i++)
        {
            float angle = 2 * Mathf.PI * i / segments;
            float outerX = Mathf.Cos(angle) * radius;
            float outerZ = Mathf.Sin(angle) * radius;
            float innerX = Mathf.Cos(angle) * innerRadius;
            float innerZ = Mathf.Sin(angle) * innerRadius;
            float u = (float)i / (segments - 1);

            // Vertices
            vertices[i] = new Vector3(outerX, 0, outerZ);                 // Outer bottom
            vertices[i + segments] = new Vector3(outerX, height, outerZ); // Outer top
            vertices[i + segments * 2] = new Vector3(innerX, 0, innerZ);  // Inner bottom
            vertices[i + segments * 3] = new Vector3(innerX, height, innerZ); // Inner top

            // UVs
            uvs[i] = new Vector2(u, 0);
            uvs[i + segments] = new Vector2(u, 1);
            uvs[i + segments * 2] = new Vector2(u, 0);
            uvs[i + segments * 3] = new Vector2(u, 1);

            // Normals (outward for outer, inward for inner)
            Vector3 outerNormal = new Vector3(outerX / radius, 0, outerZ / radius).normalized;
            Vector3 innerNormal = -outerNormal; // Inward for inner wall
            normals[i] = outerNormal;           // Outer bottom
            normals[i + segments] = outerNormal; // Outer top
            normals[i + segments * 2] = innerNormal; // Inner bottom
            normals[i + segments * 3] = innerNormal; // Inner top
        }

        // Define Triangles
        for (int i = 0; i < segments; i++)
        {
            int next = (i + 1) % segments;
            int triIndex = i * 24;

            // Outer wall (facing outward)
            triangles[triIndex] = i;
            triangles[triIndex + 1] = i + segments;
            triangles[triIndex + 2] = next + segments;
            triangles[triIndex + 3] = i;
            triangles[triIndex + 4] = next + segments;
            triangles[triIndex + 5] = next;

            // Inner wall (facing inward)
            triangles[triIndex + 6] = i + segments * 2;        // Bottom inner
            triangles[triIndex + 7] = next + segments * 3;     // Next top inner
            triangles[triIndex + 8] = i + segments * 3;        // Top inner
            triangles[triIndex + 9] = i + segments * 2;        // Bottom inner
            triangles[triIndex + 10] = next + segments * 2;    // Next bottom inner
            triangles[triIndex + 11] = next + segments * 3;    // Next top inner

            // Top surface (facing up)
            triangles[triIndex + 12] = i + segments;
            triangles[triIndex + 13] = next + segments;
            triangles[triIndex + 14] = next + segments * 3;
            triangles[triIndex + 15] = i + segments;
            triangles[triIndex + 16] = next + segments * 3;
            triangles[triIndex + 17] = i + segments * 3;

            // Bottom surface (facing down)
            triangles[triIndex + 18] = i;
            triangles[triIndex + 19] = next;
            triangles[triIndex + 20] = next + segments * 2;
            triangles[triIndex + 21] = i;
            triangles[triIndex + 22] = next + segments * 2;
            triangles[triIndex + 23] = i + segments * 2;
        }

        wallMesh.vertices = vertices;
        wallMesh.triangles = triangles;
        wallMesh.uv = uvs;
        wallMesh.normals = normals; // Assign custom normals
        // No RecalculateNormals() to preserve our manual normals

        // Floor (unchanged)
        GameObject floor = transform.Find("Floor")?.gameObject;
        if (floor == null)
        {
            floor = new GameObject("Floor");
            floor.transform.parent = transform;
            floor.transform.localPosition = Vector3.zero;
            floor.AddComponent<MeshFilter>();
            floor.AddComponent<MeshRenderer>();
        }

        Mesh floorMesh = new Mesh();
        floor.GetComponent<MeshFilter>().mesh = floorMesh;

        Vector3[] floorVertices = new Vector3[segments + 1];
        Vector2[] floorUvs = new Vector2[segments + 1];
        int[] floorTriangles = new int[segments * 3];

        floorVertices[0] = Vector3.zero;
        floorUvs[0] = new Vector2(0.5f, 0.5f);
        for (int i = 0; i < segments; i++)
        {
            float angle = 2 * Mathf.PI * i / segments;
            float x = Mathf.Cos(angle) * innerRadius;
            float z = Mathf.Sin(angle) * innerRadius;
            floorVertices[i + 1] = new Vector3(x, 0, z);
            float u = (Mathf.Cos(angle) + 1) * 0.5f;
            float v = (Mathf.Sin(angle) + 1) * 0.5f;
            floorUvs[i + 1] = new Vector2(u, v);
        }

        for (int i = 0; i < segments; i++)
        {
            int triIndex = i * 3;
            floorTriangles[triIndex] = 0;
            floorTriangles[triIndex + 1] = i + 1;
            floorTriangles[triIndex + 2] = (i + 2) > segments ? 1 : (i + 2);
        }

        floorMesh.vertices = floorVertices;
        floorMesh.triangles = floorTriangles;
        floorMesh.uv = floorUvs;
        floorMesh.RecalculateNormals();
    }
}