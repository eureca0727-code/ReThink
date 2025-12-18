using UnityEngine;
using System.Collections;

public class ChasingEnemy : MonoBehaviour
{
<<<<<<< HEAD
    [Header("ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½")]
    public float chaseSpeed = 6f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;  // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ß´ï¿½ ï¿½Ã°ï¿½

    [Header("ï¿½ï¿½Ö¹ï¿½ ï¿½ï¿½ï¿½ï¿½")]
=======
    [Header("ÃßÀû ¼³Á¤")]
    public float chaseSpeed = 6f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;  // °ø°Ý ÈÄ ¸ØÃß´Â ½Ã°£

    [Header("Àå¾Ö¹° °¨Áö")]
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
    public LayerMask obstacleLayer;
    public float obstacleDetectionDistance = 1f;

    private Transform target;
    private Rigidbody2D rb;
<<<<<<< HEAD
    private bool isAttacking = false;  // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ (ï¿½ï¿½ï¿½ï¿½ï¿½Ö´ï¿½ï¿½ï¿½)
=======
    private bool isAttacking = false;  // °ø°Ý ÁßÀÎÁö (¸ØÃçÀÖ´ÂÁö)
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

<<<<<<< HEAD
        // Å¸ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ê¾ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ Ã£ï¿½ï¿½
=======
        // Å¸°ÙÀÌ ¼³Á¤µÇÁö ¾Ê¾ÒÀ¸¸é ÇÃ·¹ÀÌ¾î Ã£±â
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
<<<<<<< HEAD
                Debug.Log($"{gameObject.name}: ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ Å¸ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Ï·ï¿½");
            }
            else
            {
                Debug.LogError($"{gameObject.name}: ï¿½Ã·ï¿½ï¿½Ì¾î¸¦ Ã£ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï´ï¿½!");
=======
                Debug.Log($"{gameObject.name}: ÇÃ·¹ÀÌ¾î Å¸°Ù ¼³Á¤ ¿Ï·á");
            }
            else
            {
                Debug.LogError($"{gameObject.name}: ÇÃ·¹ÀÌ¾î¸¦ Ã£À» ¼ö ¾ø½À´Ï´Ù!");
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
            }
        }
    }

    private void Update()
    {
        if (target == null) return;

<<<<<<< HEAD
        // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Ì¸ï¿½ ï¿½ï¿½ï¿½ï¿½
=======
        // °ø°Ý ÁßÀÌ¸é ¸ØÃã
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        if (isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

<<<<<<< HEAD
        // ï¿½×»ï¿½ ï¿½ï¿½ï¿½ï¿½ (ï¿½Å¸ï¿½, ï¿½Ã¾ß°ï¿½ Ã¼Å© ï¿½ï¿½ï¿½ï¿½)
        ChaseTarget();

        // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ Ã¼Å©
=======
        // Ç×»ó ÃßÀû (°Å¸®, ½Ã¾ß°¢ Ã¼Å© ¾øÀ½)
        ChaseTarget();

        // °ø°Ý ¹üÀ§ Ã¼Å©
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget <= attackRange)
        {
            AttackTarget();
        }
    }

<<<<<<< HEAD
    // Å¸ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ (ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½)
=======
    // Å¸°Ù ÃßÀû (¹«Á¶°Ç ÃßÀû)
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
    private void ChaseTarget()
    {
        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;

<<<<<<< HEAD
        // ï¿½ï¿½Ö¹ï¿½ È¸ï¿½ï¿½ ï¿½Ãµï¿½ (ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½)
        Vector2 finalDirection = AvoidObstacles(direction);

        // ï¿½Ìµï¿½
        rb.linearVelocity = finalDirection * chaseSpeed;

        // ï¿½ï¿½ È¸ï¿½ï¿½ (Å¸ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½)
=======
        // Àå¾Ö¹° È¸ÇÇ ½Ãµµ (°£´ÜÇÑ ¹æ¹ý)
        Vector2 finalDirection = AvoidObstacles(direction);

        // ÀÌµ¿
        rb.linearVelocity = finalDirection * chaseSpeed;

        // Àû È¸Àü (Å¸°Ù ¹æÇâÀ¸·Î)
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

<<<<<<< HEAD
    // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Ö¹ï¿½ È¸ï¿½ï¿½
    private Vector2 AvoidObstacles(Vector2 desiredDirection)
    {
        // ï¿½ï¿½ï¿½ï¿½ Ã¼Å©
=======
    // °£´ÜÇÑ Àå¾Ö¹° È¸ÇÇ
    private Vector2 AvoidObstacles(Vector2 desiredDirection)
    {
        // Àü¹æ Ã¼Å©
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        RaycastHit2D hitForward = Physics2D.Raycast(
            transform.position,
            desiredDirection,
            obstacleDetectionDistance,
            obstacleLayer
        );

<<<<<<< HEAD
        // ï¿½ï¿½Ö¹ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½×´ï¿½ï¿½ ï¿½Ìµï¿½
=======
        // Àå¾Ö¹°ÀÌ ¾øÀ¸¸é ±×´ë·Î ÀÌµ¿
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        if (hitForward.collider == null)
        {
            return desiredDirection;
        }

<<<<<<< HEAD
        // ï¿½ï¿½Ö¹ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Â¿ï¿½ï¿½ È¸ï¿½ï¿½ ï¿½Ãµï¿½
=======
        // Àå¾Ö¹°ÀÌ ÀÖÀ¸¸é ÁÂ¿ì·Î È¸ÇÇ ½Ãµµ
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
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

<<<<<<< HEAD
        // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
=======
        // ¿ÞÂÊÀÌ ºñ¾úÀ¸¸é ¿ÞÂÊÀ¸·Î
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        if (hitLeft.collider == null)
        {
            return leftDirection;
        }

<<<<<<< HEAD
        // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
=======
        // ¿À¸¥ÂÊÀÌ ºñ¾úÀ¸¸é ¿À¸¥ÂÊÀ¸·Î
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        if (hitRight.collider == null)
        {
            return rightDirection;
        }

<<<<<<< HEAD
        // ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½×³ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ (ï¿½ï¿½ï¿½ï¿½ ï¿½Ð°ï¿½ ï¿½ï¿½)
        return desiredDirection;
    }

    // Å¸ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
    private void AttackTarget()
    {
        // ï¿½Ì¹ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Ì¸ï¿½ ï¿½ßºï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
        if (isAttacking) return;

        Debug.Log($"{gameObject.name}: ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ ï¿½ï¿½ï¿½ï¿½!");

        // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Â·ï¿½ ï¿½ï¿½È¯ (ï¿½ï¿½ï¿½ï¿½)
        StartCoroutine(AttackCooldownCoroutine());

        // QTE ï¿½Ç´ï¿½ Game Over
=======
        // µÑ ´Ù ¸·ÇûÀ¸¸é ±×³É ¿ø·¡ ¹æÇâÀ¸·Î (º®À» ¹Ð°Ô µÊ)
        return desiredDirection;
    }

    // Å¸°Ù °ø°Ý
    private void AttackTarget()
    {
        // ÀÌ¹Ì °ø°Ý ÁßÀÌ¸é Áßº¹ ½ÇÇà ¹æÁö
        if (isAttacking) return;

        Debug.Log($"{gameObject.name}: ÇÃ·¹ÀÌ¾î °ø°Ý!");

        // °ø°Ý »óÅÂ·Î ÀüÈ¯ (¸ØÃã)
        StartCoroutine(AttackCooldownCoroutine());

        // QTE ¶Ç´Â Game Over
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        if (QTEManager.Instance != null)
        {
            QTEManager.Instance.TriggerQTE();
        }
    }

<<<<<<< HEAD
    // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½Ù¿ï¿½ (1ï¿½Ê°ï¿½ ï¿½ï¿½ï¿½ï¿½)
    private IEnumerator AttackCooldownCoroutine()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;  // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½

        Debug.Log($"{gameObject.name}: 1ï¿½Ê°ï¿½ ï¿½ï¿½ï¿½ï¿½");
=======
    // °ø°Ý ÈÄ Äð´Ù¿î (1ÃÊ°£ ¸ØÃã)
    private IEnumerator AttackCooldownCoroutine()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;  // ¿ÏÀüÈ÷ ¸ØÃã

        Debug.Log($"{gameObject.name}: 1ÃÊ°£ ¸ØÃã");
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
<<<<<<< HEAD
        Debug.Log($"{gameObject.name}: ï¿½Ù½ï¿½ ï¿½ß°ï¿½ ï¿½ï¿½ï¿½ï¿½");
    }

    // Å¸ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ (EnemySpawnManagerï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½)
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log($"{gameObject.name}: Å¸ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ - {newTarget.name}");
    }

    // ï¿½ï¿½ï¿½ï¿½×¿ï¿½ ï¿½Ã°ï¿½È­
=======
        Debug.Log($"{gameObject.name}: ´Ù½Ã Ãß°Ý ½ÃÀÛ");
    }

    // Å¸°Ù ¼³Á¤ (EnemySpawnManager¿¡¼­ »ç¿ë)
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log($"{gameObject.name}: Å¸°Ù ¼³Á¤µÊ - {newTarget.name}");
    }

    // µð¹ö±×¿ë ½Ã°¢È­
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
    private void OnDrawGizmos()
    {
        if (target == null) return;

<<<<<<< HEAD
        // Å¸ï¿½Ù±ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½×¸ï¿½ï¿½ï¿½
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);

        // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // ï¿½ï¿½Ö¹ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½
=======
        // Å¸°Ù±îÁö ¼± ±×¸®±â
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);

        // °ø°Ý ¹üÀ§
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Àå¾Ö¹° °¨Áö ¹üÀ§
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction * obstacleDetectionDistance);
    }
<<<<<<< HEAD

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ì¶©ëŒí•œ ëŒ€ìƒì´ ì (EnemyBody)ì¸ì§€ í™•ì¸
        if (collision.gameObject.CompareTag("EnemyBody"))
        {
            // ìƒëŒ€ë°©ì´ ì•„êµ°(Ally)ì´ ëœ ì  íŒ¨íŠ¸ë¡¤ì¸ì§€ í™•ì¸
            EnemyVisionShare visionShare = collision.gameObject.GetComponentInParent<EnemyVisionShare>();

            if (visionShare != null && visionShare.IsAlly())
            {
                // ì´ë¯¸ ê³µê²©/ë©ˆì¶¤ ìƒíƒœê°€ ì•„ë‹ ë•Œë§Œ ì‹¤í–‰
                if (!isAttacking)
                {
                    Debug.Log($"{gameObject.name}: ì•„êµ° ì ê³¼ ì¶©ëŒ! 1ì´ˆê°„ ì •ì§€í•©ë‹ˆë‹¤.");
                    StartCoroutine(AttackCooldownCoroutine()); // ê¸°ì¡´ 1ì´ˆ ì •ì§€ ì½”ë£¨í‹´ ìž¬ì‚¬ìš©
                }
            }
        }
    }
=======
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
}