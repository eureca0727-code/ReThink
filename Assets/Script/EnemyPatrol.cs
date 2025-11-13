using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("순찰 설정")]
    public Transform[] patrolPoints;  // 순찰 지점들
    public float moveSpeed = 2f;      // 이동 속도
    public float rotationSpeed = 5f;  // 회전 속도
    public float waitTime = 1f;       // 각 지점에서 대기 시간

    private int currentPointIndex = 0;
    private float waitTimer = 1f;
    private bool isWaiting = false;

    void Update()
    {
        if (patrolPoints.Length == 0) return;

        if (isWaiting)
        {
            // 대기 중
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                // 다음 지점으로
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            }
        }
        else
        {
            // 목표 지점으로 이동
            Transform targetPoint = patrolPoints[currentPointIndex];
            Vector3 direction = (targetPoint.position - transform.position).normalized;

            // 이동
            transform.position += direction * moveSpeed * Time.deltaTime;

            // 회전 
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 30); // 스프라이트 방향에 따라 조정
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // 목표 지점 도착 확인
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                isWaiting = true;
            }
        }
    }
}