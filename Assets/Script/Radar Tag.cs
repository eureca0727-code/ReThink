using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyRadar : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionCooldown = 2f; // QTE 실패 후 다시 감지되기까지 쿨타임

    private bool canDetect = true;
    private float cooldownTimer = 0f;

    void Update()
    {
        // 쿨타임 체크
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
        // 플레이어와 충돌했는지 확인
        if (other.CompareTag("Player") && canDetect)
        {
            TriggerDetection();
        }
    }

    void TriggerDetection()
    {
        Debug.Log($"[{transform.parent.name}] 플레이어 감지!");

        // QTE 발동
        if (QTEManager.Instance != null)
        {
            QTEManager.Instance.TriggerQTE();

            // 쿨타임 시작 (QTE 중복 발동 방지)
            canDetect = false;
            cooldownTimer = detectionCooldown;
        }
        else
        {
            // QTE 매니저가 없으면 기존처럼 바로 게임오버
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}