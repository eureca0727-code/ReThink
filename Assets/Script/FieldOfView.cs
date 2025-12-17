using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class FieldOfView : MonoBehaviour
{
    [Range(0, 360)] public float fov = 90f;
    public int rayCount = 90;
    public float viewDistance = 10f;
    public LayerMask layerMask;
    public GameObject DarknessOverlay;

    private Mesh mesh;

    private void Start()
    {
        // 어둠 오버레이 켜두기
        DarknessOverlay.SetActive(true);

        mesh = new Mesh();
        mesh.name = "FOV";
        GetComponent<MeshFilter>().mesh = mesh;

        // ★ 핵심: FOV Mesh가 DarknessOverlay보다 앞에 렌더링되도록 설정
        var mr = GetComponent<MeshRenderer>();
        mr.material.renderQueue = 3001; // Overlay(4000)보다 낮고, 월드(2000)보다 높게
    }

    private void LateUpdate()
    {
        DrawFOV();
    }

    private void DrawFOV()
    {
        float angle = transform.eulerAngles.z + (fov / 2f);
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = Vector3.zero;
        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, GetVectorFromAngle(angle), viewDistance, layerMask);
            Vector3 vertex;

            if (hit.collider == null)
                vertex = GetVectorFromAngle(angle) * viewDistance;
            else
                vertex = hit.point - (Vector2)transform.position;

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}
