using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QTEManager : MonoBehaviour
{
    public static QTEManager Instance;

    [Header("QTE Settings")]
    public KeyCode[] possibleKeys = new KeyCode[]
    {
        KeyCode.Q, KeyCode.E, KeyCode.R, KeyCode.T,
        KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P,
        KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V,
        KeyCode.B, KeyCode.N, KeyCode.M
    };

    [Header("Time Settings")]
    public float initialTime = 3f;
    public float minimumTime = 1f;
    public float difficultyFactor = 0.5f;

    [Header("Slow Motion Settings")]
    public bool useSlowMotion = true;
    public float slowMotionScale = 0.2f;
    public float slowMotionTransitionSpeed = 5f;

    [Header("Player Control")]
    public MonoBehaviour playerController;
    public Rigidbody2D playerRigidbody;

    [Header("UI References")]
    public GameObject qtePanel;
    public TextMeshProUGUI keyText;
    public Image timerBar;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI detectionCountText;

    private bool qteActive = false;
    private KeyCode currentKey;
    private float timeRemaining;
    private float maxTime;
    private int detectionCount = 0;
    private float targetTimeScale = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (qtePanel != null)
        {
            qtePanel.SetActive(false);
        }

        if (playerController == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerController = player.GetComponent<PlayerController>();
            }
        }

        if (playerRigidbody == null && playerController != null)
        {
            playerRigidbody = playerController.GetComponent<Rigidbody2D>();
        }

        UpdateDetectionCountUI();
    }

    void Update()
    {
        // 대화 중이면 QTE 로직 실행 안 함
        if (!qteActive && Time.timeScale < 0.01f)
        {
            return;
        }

        if (useSlowMotion)
        {
            Time.timeScale = Mathf.Lerp(
                Time.timeScale,
                targetTimeScale,
                slowMotionTransitionSpeed * Time.unscaledDeltaTime
            );
        }

        if (qteActive)
        {
            timeRemaining -= Time.unscaledDeltaTime;
            UpdateTimerUI();

            if (timeRemaining <= 0f)
            {
                FailQTE();
                return;
            }

            if (Input.GetKeyDown(currentKey))
            {
                SuccessQTE();
            }
            else if (Input.anyKeyDown)
            {
                FailQTE();
            }
        }
    }

    public void TriggerQTE()
    {
        if (qteActive) return;

        qteActive = true;
        detectionCount++;

        if (useSlowMotion)
        {
            targetTimeScale = slowMotionScale;
        }

        FreezePlayer();

        currentKey = possibleKeys[Random.Range(0, possibleKeys.Length)];
        maxTime = CalculateQTETime();
        timeRemaining = maxTime;

        ShowQTE();

        Debug.Log($"[QTE] 발각 {detectionCount}번째 - 키: {currentKey}, 시간: {maxTime:F2}초");
    }

    void FreezePlayer()
    {
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        if (playerRigidbody != null)
        {
            playerRigidbody.linearVelocity = Vector2.zero;
            playerRigidbody.angularVelocity = 0f;
        }

        Debug.Log("[QTE] 플레이어 조작 비활성화");
    }

    void UnfreezePlayer()
    {
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        Debug.Log("[QTE] 플레이어 조작 활성화");
    }

    float CalculateQTETime()
    {
        float baseTime = initialTime - minimumTime;
        float time = minimumTime + baseTime / (1f + (detectionCount - 1) * difficultyFactor);
        return Mathf.Max(time, minimumTime);
    }

    void ShowQTE()
    {
        if (qtePanel != null)
        {
            qtePanel.SetActive(true);
        }

        if (keyText != null)
        {
            keyText.text = currentKey.ToString();
        }

        UpdateDetectionCountUI();
        UpdateTimerUI();
    }

    void HideQTE()
    {
        if (qtePanel != null)
        {
            qtePanel.SetActive(false);
        }

        qteActive = false;

        if (useSlowMotion)
        {
            targetTimeScale = 1f;
        }

        UnfreezePlayer();
    }

    void UpdateTimerUI()
    {
        if (timerBar != null)
        {
            timerBar.fillAmount = timeRemaining / maxTime;

            if (timeRemaining / maxTime > 0.5f)
            {
                timerBar.color = Color.green;
            }
            else if (timeRemaining / maxTime > 0.25f)
            {
                timerBar.color = Color.yellow;
            }
            else
            {
                timerBar.color = Color.red;
            }
        }

        if (timerText != null)
        {
            timerText.text = timeRemaining.ToString("F2") + "s";
        }
    }

    void UpdateDetectionCountUI()
    {
        if (detectionCountText != null)
        {
            detectionCountText.text = $"발각 횟수: {detectionCount}";
        }
    }

    void SuccessQTE()
    {
        Debug.Log("[QTE] 성공! 위기 탈출!");
        HideQTE();
    }

    void FailQTE()
    {
        Debug.Log("[QTE] 실패! 게임 오버!");
        Time.timeScale = 1f;

        UnfreezePlayer();

        Invoke("GameOver", 0.5f);
    }

    void GameOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
        UnfreezePlayer();
    }
}