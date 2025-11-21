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
        Debug.Log(gameObject.name + " - EnemyPatrol 시작!");

        if (patrolPoints.Length > 0)
        {
            Vector3 direction = (patrolPoints[currentPointIndex].position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    void Update()
    {
        // 순찰 지점이 없으면 종료
        if (patrolPoints.Length == 0)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.LogWarning(gameObject.name + " - 순찰 지점이 없습니다!");
            }
            return;
        }

        // TimeScale이 0이면 멈춤
        if (Time.timeScale <= 0.01f)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log(gameObject.name + " - TimeScale이 0이므로 멈춤!");
            }
            return;
        }

        // 여기까지 왔다면 패트롤 실행 중
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(gameObject.name + " - 패트롤 실행 중!");
        }

        // 대기 중
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
        // 이동 중
        else
        {
            Transform targetPoint = patrolPoints[currentPointIndex];
            Vector3 direction = (targetPoint.position - transform.position).normalized;

            // 이동
            transform.position += direction * moveSpeed * Time.deltaTime;

            // 회전
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // 도착 체크
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                isWaiting = true;
            }
        }
    }
}
