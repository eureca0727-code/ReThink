using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Z축은 원래 카메라 위치 유지 (2D용)
        smoothedPosition.z = transform.position.z;

        transform.position = smoothedPosition;
    }
}