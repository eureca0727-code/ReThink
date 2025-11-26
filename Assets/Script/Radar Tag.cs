using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyRadar : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionCooldown = 2f;
    public LayerMask wallLayerMask;  // Obstacle 레이어 체크!

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
        if (other.CompareTag("Player") && canDetect)
        {
            // 대시 중인지 확인
            if (IsPlayerDashing(other))
            {
                Debug.Log($"[{transform.parent.name}] 플레이어가 대시 중 - 감지 무시!");
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
            // 대시 중인지 확인
            if (IsPlayerDashing(other))
            {
                Debug.Log($"[{transform.parent.name}] 플레이어가 대시 중 - 감지 무시!");
                return;
            }

            if (CanSeePlayer(other.transform.position))
            {
                TriggerDetection();
            }
        }
    }

    // 플레이어가 대시 중인지 확인
    bool IsPlayerDashing(Collider2D playerCollider)
    {
        PlayerController playerController = playerCollider.GetComponent<PlayerController>();
        if (playerController != null)
        {
            return playerController.IsDashing();
        }
        return false;
    }

    // 벽 체크: 적 본체 → 플레이어 사이에 벽 있는지
    bool CanSeePlayer(Vector3 playerPos)
    {
        // 레이더 위치가 아닌, 적 본체(부모) 위치에서 Raycast
        Vector2 startPos = transform.parent != null
            ? (Vector2)transform.parent.position
            : (Vector2)transform.position;

        Vector2 direction = (Vector2)playerPos - startPos;
        float distance = direction.magnitude;

        // 디버그: Scene 뷰에서 Raycast 시각화 (빨간 선)
        Debug.DrawRay(startPos, direction, Color.red, 0.5f);

        RaycastHit2D hit = Physics2D.Raycast(
            startPos,
            direction.normalized,
            distance,
            wallLayerMask
        );

        // 디버그: 무엇에 부딪혔는지 확인
        if (hit.collider != null)
        {
            Debug.Log($"[Raycast] 벽 감지됨: {hit.collider.name}, Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");
            return false;  // 벽에 막힘 → 감지 안 함
        }
        else
        {
            Debug.Log("[Raycast] 벽 없음 - 플레이어 보임!");
            return true;   // 벽 없음 → 감지함
        }
    }

    void TriggerDetection()
    {
        Debug.Log($"[{transform.parent.name}] 플레이어 감지!");

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