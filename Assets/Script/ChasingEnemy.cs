using UnityEngine;
using System.Collections;

public class ChasingEnemy : MonoBehaviour
{
    [Header("추적 설정")]
    public float chaseSpeed = 6f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;  // 공격 후 멈추는 시간

    [Header("장애물 감지")]
    public LayerMask obstacleLayer;
    public float obstacleDetectionDistance = 1f;

    private Transform target;
    private Rigidbody2D rb;
    private bool isAttacking = false;  // 공격 중인지 (멈춰있는지)

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 타겟이 설정되지 않았으면 플레이어 찾기
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
                Debug.Log($"{gameObject.name}: 플레이어 타겟 설정 완료");
            }
            else
            {
                Debug.LogError($"{gameObject.name}: 플레이어를 찾을 수 없습니다!");
            }
        }
    }

    private void Update()
    {
        if (target == null) return;

        // 공격 중이면 멈춤
        if (isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // 항상 추적 (거리, 시야각 체크 없음)
        ChaseTarget();

        // 공격 범위 체크
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget <= attackRange)
        {
            AttackTarget();
        }
    }

    // 타겟 추적 (무조건 추적)
    private void ChaseTarget()
    {
        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;

        // 장애물 회피 시도 (간단한 방법)
        Vector2 finalDirection = AvoidObstacles(direction);

        // 이동
        rb.linearVelocity = finalDirection * chaseSpeed;

        // 적 회전 (타겟 방향으로)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    // 간단한 장애물 회피
    private Vector2 AvoidObstacles(Vector2 desiredDirection)
    {
        // 전방 체크
        RaycastHit2D hitForward = Physics2D.Raycast(
            transform.position,
            desiredDirection,
            obstacleDetectionDistance,
            obstacleLayer
        );

        // 장애물이 없으면 그대로 이동
        if (hitForward.collider == null)
        {
            return desiredDirection;
        }

        // 장애물이 있으면 좌우로 회피 시도
        Vector2 leftDirection = Quaternion.Euler(0, 0, 45) * desiredDirection;
        Vector2 rightDirection = Quaternion.Euler(0, 0, -45) * desiredDirection;

        RaycastHit2D hitLeft = Physics2D.Raycast(
            transform.position,
            leftDirection,
            obstacleDetectionDistance,
            obstacleLayer
        );

        RaycastHit2D hitRight = Physics2D.Raycast(
            transform.position,
            rightDirection,
            obstacleDetectionDistance,
            obstacleLayer
        );

        // 왼쪽이 비었으면 왼쪽으로
        if (hitLeft.collider == null)
        {
            return leftDirection;
        }

        // 오른쪽이 비었으면 오른쪽으로
        if (hitRight.collider == null)
        {
            return rightDirection;
        }

        // 둘 다 막혔으면 그냥 원래 방향으로 (벽을 밀게 됨)
        return desiredDirection;
    }

    // 타겟 공격
    private void AttackTarget()
    {
        // 이미 공격 중이면 중복 실행 방지
        if (isAttacking) return;

        Debug.Log($"{gameObject.name}: 플레이어 공격!");

        // 공격 상태로 전환 (멈춤)
        StartCoroutine(AttackCooldownCoroutine());

        // QTE 또는 Game Over
        if (QTEManager.Instance != null)
        {
            QTEManager.Instance.TriggerQTE();
        }
    }

    // 공격 후 쿨다운 (1초간 멈춤)
    private IEnumerator AttackCooldownCoroutine()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;  // 완전히 멈춤

        Debug.Log($"{gameObject.name}: 1초간 멈춤");

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        Debug.Log($"{gameObject.name}: 다시 추격 시작");
    }

    // 타겟 설정 (EnemySpawnManager에서 사용)
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log($"{gameObject.name}: 타겟 설정됨 - {newTarget.name}");
    }

    // 디버그용 시각화
    private void OnDrawGizmos()
    {
        if (target == null) return;

        // 타겟까지 선 그리기
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);

        // 공격 범위
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // 장애물 감지 범위
        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction * obstacleDetectionDistance);
    }
}