using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("순찰 설정")]
    public Transform[] patrolPoints;
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    public float waitTime = 1f;

    private int currentPointIndex = 0;
    private float waitTimer = 1f;
    private bool isWaiting = false;

    void Start()
    {
        // 게임 시작 시 첫 번째 순찰 지점을 향하도록 회전 설정
        if (patrolPoints.Length > 0)
        {
            Vector3 direction = (patrolPoints[currentPointIndex].position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle); // Update()와 동일한 오프셋 사용
            }
        }
    }

    void Update()
    {
        if (patrolPoints.Length == 0) return;

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            }
        }
        else
        {
            Transform targetPoint = patrolPoints[currentPointIndex];
            Vector3 direction = (targetPoint.position - transform.position).normalized;

            transform.position += direction * moveSpeed * Time.deltaTime;

            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                isWaiting = true;
            }
        }
    }
}