using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyRadar : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionCooldown = 2f;
    public LayerMask wallLayerMask;  // Obstacle ���̾� üũ!

    private bool canDetect = true;
    private float cooldownTimer = 0f;

    void Update()
    {
        if (!canDetect)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canDetect = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (EnemyInteraction.IsInDialogue) return;

        if (other.CompareTag("Player") && canDetect)
        {
            // ��� ������ Ȯ��
            if (IsPlayerDashing(other))
            {
                Debug.Log($"[{transform.parent.name}] �÷��̾ ��� �� - ���� ����!");
                return;
            }

            if (CanSeePlayer(other.transform.position))
            {
                TriggerDetection();
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canDetect)
        {
            if (EnemyInteraction.IsInDialogue) return;

            // ��� ������ Ȯ��
            if (IsPlayerDashing(other))
            {
                Debug.Log($"[{transform.parent.name}] �÷��̾ ��� �� - ���� ����!");
                return;
            }

            if (CanSeePlayer(other.transform.position))
            {
                TriggerDetection();
            }
        }
    }

    // �÷��̾ ��� ������ Ȯ��
    bool IsPlayerDashing(Collider2D playerCollider)
    {
        PlayerController playerController = playerCollider.GetComponent<PlayerController>();
        if (playerController != null)
        {
            return playerController.IsDashing();
        }
        return false;
    }

    // �� üũ: �� ��ü �� �÷��̾� ���̿� �� �ִ���
    bool CanSeePlayer(Vector3 playerPos)
    {
        // ���̴� ��ġ�� �ƴ�, �� ��ü(�θ�) ��ġ���� Raycast
        Vector2 startPos = transform.parent != null
            ? (Vector2)transform.parent.position
            : (Vector2)transform.position;

        Vector2 direction = (Vector2)playerPos - startPos;
        float distance = direction.magnitude;

        // �����: Scene �信�� Raycast �ð�ȭ (���� ��)
        Debug.DrawRay(startPos, direction, Color.red, 0.5f);

        RaycastHit2D hit = Physics2D.Raycast(
            startPos,
            direction.normalized,
            distance,
            wallLayerMask
        );

        // �����: ������ �ε������� Ȯ��
        if (hit.collider != null)
        {
            Debug.Log($"[Raycast] �� ������: {hit.collider.name}, Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");
            return false;  // ���� ���� �� ���� �� ��
        }
        else
        {
            Debug.Log("[Raycast] �� ���� - �÷��̾� ����!");
            return true;   // �� ���� �� ������
        }
    }

    void TriggerDetection()
    {
        Debug.Log($"[{transform.parent.name}] �÷��̾� ����!");

        if (QTEManager.Instance != null)
        {
            QTEManager.Instance.TriggerQTE();
            canDetect = false;
            cooldownTimer = detectionCooldown;
        }
        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}