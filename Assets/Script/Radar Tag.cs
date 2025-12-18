using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyRadar : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionCooldown = 2f;
<<<<<<< HEAD
    public LayerMask wallLayerMask;  // Obstacle ï¿½ï¿½ï¿½Ì¾ï¿½ Ã¼Å©!
=======
    public LayerMask wallLayerMask;  // Obstacle ·¹ÀÌ¾î Ã¼Å©!
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0

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
<<<<<<< HEAD
        if (EnemyInteraction.IsInDialogue) return;

        if (other.CompareTag("Player") && canDetect)
        {
            // ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ È®ï¿½ï¿½
            if (IsPlayerDashing(other))
            {
                Debug.Log($"[{transform.parent.name}] ï¿½Ã·ï¿½ï¿½Ì¾î°¡ ï¿½ï¿½ï¿½ ï¿½ï¿½ - ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½!");
=======
        if (other.CompareTag("Player") && canDetect)
        {
            // ´ë½Ã ÁßÀÎÁö È®ÀÎ
            if (IsPlayerDashing(other))
            {
                Debug.Log($"[{transform.parent.name}] ÇÃ·¹ÀÌ¾î°¡ ´ë½Ã Áß - °¨Áö ¹«½Ã!");
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
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
<<<<<<< HEAD
            if (EnemyInteraction.IsInDialogue) return;

            // ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ È®ï¿½ï¿½
            if (IsPlayerDashing(other))
            {
                Debug.Log($"[{transform.parent.name}] ï¿½Ã·ï¿½ï¿½Ì¾î°¡ ï¿½ï¿½ï¿½ ï¿½ï¿½ - ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½!");
=======
            // ´ë½Ã ÁßÀÎÁö È®ÀÎ
            if (IsPlayerDashing(other))
            {
                Debug.Log($"[{transform.parent.name}] ÇÃ·¹ÀÌ¾î°¡ ´ë½Ã Áß - °¨Áö ¹«½Ã!");
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
                return;
            }

            if (CanSeePlayer(other.transform.position))
            {
                TriggerDetection();
            }
        }
    }

<<<<<<< HEAD
    // ï¿½Ã·ï¿½ï¿½Ì¾î°¡ ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ È®ï¿½ï¿½
=======
    // ÇÃ·¹ÀÌ¾î°¡ ´ë½Ã ÁßÀÎÁö È®ÀÎ
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
    bool IsPlayerDashing(Collider2D playerCollider)
    {
        PlayerController playerController = playerCollider.GetComponent<PlayerController>();
        if (playerController != null)
        {
            return playerController.IsDashing();
        }
        return false;
    }

<<<<<<< HEAD
    // ï¿½ï¿½ Ã¼Å©: ï¿½ï¿½ ï¿½ï¿½Ã¼ ï¿½ï¿½ ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ ï¿½ï¿½ï¿½Ì¿ï¿½ ï¿½ï¿½ ï¿½Ö´ï¿½ï¿½ï¿½
    bool CanSeePlayer(Vector3 playerPos)
    {
        // ï¿½ï¿½ï¿½Ì´ï¿½ ï¿½ï¿½Ä¡ï¿½ï¿½ ï¿½Æ´ï¿½, ï¿½ï¿½ ï¿½ï¿½Ã¼(ï¿½Î¸ï¿½) ï¿½ï¿½Ä¡ï¿½ï¿½ï¿½ï¿½ Raycast
=======
    // º® Ã¼Å©: Àû º»Ã¼ ¡æ ÇÃ·¹ÀÌ¾î »çÀÌ¿¡ º® ÀÖ´ÂÁö
    bool CanSeePlayer(Vector3 playerPos)
    {
        // ·¹ÀÌ´õ À§Ä¡°¡ ¾Æ´Ñ, Àû º»Ã¼(ºÎ¸ð) À§Ä¡¿¡¼­ Raycast
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        Vector2 startPos = transform.parent != null
            ? (Vector2)transform.parent.position
            : (Vector2)transform.position;

        Vector2 direction = (Vector2)playerPos - startPos;
        float distance = direction.magnitude;

<<<<<<< HEAD
        // ï¿½ï¿½ï¿½ï¿½ï¿½: Scene ï¿½ä¿¡ï¿½ï¿½ Raycast ï¿½Ã°ï¿½È­ (ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½)
=======
        // µð¹ö±×: Scene ºä¿¡¼­ Raycast ½Ã°¢È­ (»¡°£ ¼±)
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        Debug.DrawRay(startPos, direction, Color.red, 0.5f);

        RaycastHit2D hit = Physics2D.Raycast(
            startPos,
            direction.normalized,
            distance,
            wallLayerMask
        );

<<<<<<< HEAD
        // ï¿½ï¿½ï¿½ï¿½ï¿½: ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Îµï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ È®ï¿½ï¿½
        if (hit.collider != null)
        {
            Debug.Log($"[Raycast] ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½: {hit.collider.name}, Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");
            return false;  // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½
        }
        else
        {
            Debug.Log("[Raycast] ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ - ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ ï¿½ï¿½ï¿½ï¿½!");
            return true;   // ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
=======
        // µð¹ö±×: ¹«¾ù¿¡ ºÎµúÇû´ÂÁö È®ÀÎ
        if (hit.collider != null)
        {
            Debug.Log($"[Raycast] º® °¨ÁöµÊ: {hit.collider.name}, Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");
            return false;  // º®¿¡ ¸·Èû ¡æ °¨Áö ¾È ÇÔ
        }
        else
        {
            Debug.Log("[Raycast] º® ¾øÀ½ - ÇÃ·¹ÀÌ¾î º¸ÀÓ!");
            return true;   // º® ¾øÀ½ ¡æ °¨ÁöÇÔ
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        }
    }

    void TriggerDetection()
    {
<<<<<<< HEAD
        Debug.Log($"[{transform.parent.name}] ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ ï¿½ï¿½ï¿½ï¿½!");
=======
        Debug.Log($"[{transform.parent.name}] ÇÃ·¹ÀÌ¾î °¨Áö!");
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0

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