using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class FieldOfView : MonoBehaviour
{
    [Range(0, 360)] public float fov = 90f;
    public int rayCount = 90;
    public float viewDistance = 10f;
    public LayerMask layerMask;
    private Mesh mesh;
    public GameObject DarknessOverlay;

    private void Start()
    {
        DarknessOverlay.SetActive(true); // Game 시작 시에만 활성화

        mesh = new Mesh();
        mesh.name = "FOV";
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void LateUpdate()
    {
        DrawFOV();
    }

    private void DrawFOV()
    {
        float angle = fov * 0.5f;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, GetVectorFromAngle(transform.eulerAngles.z + angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                vertex = GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = GetVectorFromAngle(angle) * (raycastHit2D.distance / viewDistance) * viewDistance;
            }
            vertices[vertexIndex] = vertex;

            if (0 < i)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            ++vertexIndex;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    private int GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        int angle = Mathf.RoundToInt(n);

        return angle;
    }
}
