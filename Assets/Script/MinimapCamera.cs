using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform player;  // 플레이어 Transform

    [Header("Minimap Settings")]
    public float height = 10f;  // 카메라 높이
    public float viewSize = 10f;  // 보이는 범위 (Orthographic Size)

    [Header("Update Mode")]
    public bool smoothFollow = false;
    public float smoothSpeed = 5f;

    private Camera minimapCam;

    void Start()
    {
        minimapCam = GetComponent<Camera>();

        // 플레이어 자동 찾기
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        // Orthographic Size 설정
        if (minimapCam != null)
        {
            minimapCam.orthographicSize = viewSize;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = new Vector3(
            player.position.x,
            player.position.y,
            player.position.z - height  // Z축으로 떨어뜨림 (2D이므로)
        );

        if (smoothFollow)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                smoothSpeed * Time.deltaTime
            );
        }
        else
        {
            transform.position = targetPosition;
        }
    }

    // 미니맵 범위 조절 (런타임에서도 가능)
    public void SetViewSize(float size)
    {
        viewSize = size;
        if (minimapCam != null)
        {
            minimapCam.orthographicSize = viewSize;
        }
    }
}