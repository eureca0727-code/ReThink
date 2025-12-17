using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public Rigidbody2D PlayerRigidbody;
    public float speed = 8f;

    [Header("Dash Settings")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public KeyCode dashKey = KeyCode.LeftShift;

    [Header("Dash Visual Effects (Optional)")]
    public TrailRenderer trailRenderer;
    public float trailTime = 0.3f;

    private bool isDashing = false;
    private bool canDash = true;
    private float dashTimer = 0f;
    private Vector2 dashDirection;
    private int originalLayer;

    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        originalLayer = gameObject.layer;

        // Trail Renderer 설정 (있다면)
        if (trailRenderer != null)
        {
            trailRenderer.emitting = false;
        }

        // SoulManager의 대시 해금 이벤트 구독
        if (SoulManager.Instance != null)
        {
            SoulManager.Instance.onDashUnlocked.AddListener(OnDashUnlocked);
        }
    }

    void Update()
    {
        // 대시 중이면 대시 처리만
        if (isDashing)
        {
            HandleDash();
            return;
        }

        // 일반 이동
        HandleMovement();

        // 대시 입력 체크
        HandleDashInput();
    }

    void HandleMovement()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float ySpeed = yInput * speed;

        Vector2 newVelocity = new Vector2(xSpeed, ySpeed);
        PlayerRigidbody.linearVelocity = newVelocity;
    }

    void HandleDashInput()
    {
        // 대시가 해금되었고, 쿨다운이 끝났고, 대시 키를 누르면
        if (SoulManager.Instance != null &&
            SoulManager.Instance.IsDashUnlocked() &&
            canDash &&
            Input.GetKeyDown(dashKey))
        {
            // 현재 입력 방향
            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");

            Vector2 direction = new Vector2(xInput, yInput).normalized;

            // 입력이 없으면 현재 이동 방향 사용
            if (direction.magnitude < 0.1f)
            {
                // 움직이지 않고 있으면 마지막으로 본 방향 또는 오른쪽으로
                if (PlayerRigidbody.linearVelocity.magnitude > 0.1f)
                {
                    direction = PlayerRigidbody.linearVelocity.normalized;
                }
                else
                {
                    direction = Vector2.right; // 기본값
                }
            }

            StartDash(direction);
        }
    }

    void StartDash(Vector2 direction)
    {
        isDashing = true;
        canDash = false;
        dashTimer = dashDuration;
        dashDirection = direction;

        // 레이더 감지에서 제외 (레이어 변경)
        gameObject.layer = LayerMask.NameToLayer("DashInvisible");

        // Trail 효과 시작 (있다면)
        if (trailRenderer != null)
        {
            trailRenderer.emitting = true;
        }

        Debug.Log("대시 시작!");
    }

    void HandleDash()
    {
        dashTimer -= Time.deltaTime;

        // 대시 이동
        PlayerRigidbody.linearVelocity = dashDirection * dashSpeed;

        if (dashTimer <= 0f)
        {
            EndDash();
        }
    }

    void EndDash()
    {
        isDashing = false;

        // 원래 레이어로 복구
        gameObject.layer = originalLayer;

        // Trail 효과 종료 (있다면)
        if (trailRenderer != null)
        {
            trailRenderer.emitting = false;
        }

        Debug.Log("대시 종료!");

        // 쿨다운 시작
        Invoke(nameof(ResetDashCooldown), dashCooldown);
    }

    void ResetDashCooldown()
    {
        canDash = true;
        Debug.Log("대시 준비 완료!");
    }

    void OnDashUnlocked()
    {
        Debug.Log("PlayerController: 대시 능력 활성화됨!");
        // UI 알림 표시 등 추가 처리 가능
    }

    // EnemyRadar와 FieldOfView에서 체크할 수 있도록 public 메서드
    public bool IsDashing()
    {
        return isDashing;
    }

    void OnDestroy()
    {
        // 이벤트 구독 해제
        if (SoulManager.Instance != null)
        {
            SoulManager.Instance.onDashUnlocked.RemoveListener(OnDashUnlocked);
        }
    }
}