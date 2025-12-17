using UnityEngine;
using System.Collections;

public class ChasingEnemy : MonoBehaviour
{
    [Header("���� ����")]
    public float chaseSpeed = 6f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;  // ���� �� ���ߴ� �ð�

    [Header("��ֹ� ����")]
    public LayerMask obstacleLayer;
    public float obstacleDetectionDistance = 1f;

    private Transform target;
    private Rigidbody2D rb;
    private bool isAttacking = false;  // ���� ������ (�����ִ���)

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Ÿ���� �������� �ʾ����� �÷��̾� ã��
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
                Debug.Log($"{gameObject.name}: �÷��̾� Ÿ�� ���� �Ϸ�");
            }
            else
            {
                Debug.LogError($"{gameObject.name}: �÷��̾ ã�� �� �����ϴ�!");
            }
        }
    }

    private void Update()
    {
        if (target == null) return;

        // ���� ���̸� ����
        if (isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // �׻� ���� (�Ÿ�, �þ߰� üũ ����)
        ChaseTarget();

        // ���� ���� üũ
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget <= attackRange)
        {
            AttackTarget();
        }
    }

    // Ÿ�� ���� (������ ����)
    private void ChaseTarget()
    {
        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;

        // ��ֹ� ȸ�� �õ� (������ ���)
        Vector2 finalDirection = AvoidObstacles(direction);

        // �̵�
        rb.linearVelocity = finalDirection * chaseSpeed;

        // �� ȸ�� (Ÿ�� ��������)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    // ������ ��ֹ� ȸ��
    private Vector2 AvoidObstacles(Vector2 desiredDirection)
    {
        // ���� üũ
        RaycastHit2D hitForward = Physics2D.Raycast(
            transform.position,
            desiredDirection,
            obstacleDetectionDistance,
            obstacleLayer
        );

        // ��ֹ��� ������ �״�� �̵�
        if (hitForward.collider == null)
        {
            return desiredDirection;
        }

        // ��ֹ��� ������ �¿�� ȸ�� �õ�
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

        // ������ ������� ��������
        if (hitLeft.collider == null)
        {
            return leftDirection;
        }

        // �������� ������� ����������
        if (hitRight.collider == null)
        {
            return rightDirection;
        }

        // �� �� �������� �׳� ���� �������� (���� �а� ��)
        return desiredDirection;
    }

    // Ÿ�� ����
    private void AttackTarget()
    {
        // �̹� ���� ���̸� �ߺ� ���� ����
        if (isAttacking) return;

        Debug.Log($"{gameObject.name}: �÷��̾� ����!");

        // ���� ���·� ��ȯ (����)
        StartCoroutine(AttackCooldownCoroutine());

        // QTE �Ǵ� Game Over
        if (QTEManager.Instance != null)
        {
            QTEManager.Instance.TriggerQTE();
        }
    }

    // ���� �� ��ٿ� (1�ʰ� ����)
    private IEnumerator AttackCooldownCoroutine()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;  // ������ ����

        Debug.Log($"{gameObject.name}: 1�ʰ� ����");

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        Debug.Log($"{gameObject.name}: �ٽ� �߰� ����");
    }

    // Ÿ�� ���� (EnemySpawnManager���� ���)
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log($"{gameObject.name}: Ÿ�� ������ - {newTarget.name}");
    }

    // ����׿� �ð�ȭ
    private void OnDrawGizmos()
    {
        if (target == null) return;

        // Ÿ�ٱ��� �� �׸���
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);

        // ���� ����
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // ��ֹ� ���� ����
        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction * obstacleDetectionDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 대상이 적(EnemyBody)인지 확인
        if (collision.gameObject.CompareTag("EnemyBody"))
        {
            // 상대방이 아군(Ally)이 된 적 패트롤인지 확인
            EnemyVisionShare visionShare = collision.gameObject.GetComponentInParent<EnemyVisionShare>();

            if (visionShare != null && visionShare.IsAlly())
            {
                // 이미 공격/멈춤 상태가 아닐 때만 실행
                if (!isAttacking)
                {
                    Debug.Log($"{gameObject.name}: 아군 적과 충돌! 1초간 정지합니다.");
                    StartCoroutine(AttackCooldownCoroutine()); // 기존 1초 정지 코루틴 재사용
                }
            }
        }
    }
}