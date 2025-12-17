using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class QTEManager : MonoBehaviour
{
    public static QTEManager Instance;

    [Header("QTE Settings")]
    public KeyCode[] qteKeys = new KeyCode[]
    {
        KeyCode.W,
        KeyCode.A,
        KeyCode.S,
        KeyCode.D
    };

    [Header("Time Settings")]
    public float qteTime = 3f;

    [Header("Slow Motion Settings")]
    public bool useSlowMotion = true;
    public float slowMotionScale = 0.2f;
    public float slowMotionTransitionSpeed = 5f;

    [Header("Player Control")]
    public MonoBehaviour playerController;
    public Rigidbody2D playerRigidbody;

    [Header("UI References")]
    public GameObject qtePanel;
    public TextMeshProUGUI sequenceText;
    public Image timerBar;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI detectionCountText;

    private bool qteActive = false;
    private List<KeyCode> currentSequence = new List<KeyCode>();
    private int currentInputIndex = 0;
    private float timeRemaining;
    private int detectionCount = 0;
    private float targetTimeScale = 1f;

    // WASD 아이콘 매핑
    private Dictionary<KeyCode, string> keyIcons = new Dictionary<KeyCode, string>()
    {
        { KeyCode.W, "^" },
        { KeyCode.A, "<" },
        { KeyCode.S, "v" },
        { KeyCode.D, ">" }
    };

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
        if (!qteActive)
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

            KeyCode expectedKey = currentSequence[currentInputIndex];

            // 정답 키를 눌렀을 때만 진행
            if (Input.GetKeyDown(expectedKey))
            {
                currentInputIndex++;
                UpdateSequenceUI();

                if (currentInputIndex >= currentSequence.Count)
                {
                    SuccessQTE();
                }
            }
            // 잘못된 키를 눌러도 무시 (아무 일도 안 일어남)
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

        GenerateSequence(detectionCount);
        currentInputIndex = 0;
        timeRemaining = qteTime;

        ShowQTE();

        Debug.Log($"[QTE] 발각 {detectionCount}번째 - 시퀀스: {GetSequenceDebugString()}");
    }

    void GenerateSequence(int length)
    {
        currentSequence.Clear();
        for (int i = 0; i < length; i++)
        {
            KeyCode randomKey = qteKeys[Random.Range(0, qteKeys.Length)];
            currentSequence.Add(randomKey);
        }
    }

    string GetSequenceDebugString()
    {
        string result = "";
        foreach (KeyCode key in currentSequence)
        {
            result += keyIcons[key] + " ";
        }
        return result;
    }

    string GetSequenceString()
    {
        string result = "";
        for (int i = 0; i < currentSequence.Count; i++)
        {
            if (i < currentInputIndex)
            {
                // 이미 입력한 키는 회색으로
                result += "<color=#888888>" + keyIcons[currentSequence[i]] + "</color>  ";
            }
            else if (i == currentInputIndex)
            {
                // 현재 입력해야 할 키는 노란색 + 크게
                result += "<color=yellow><size=80>" + keyIcons[currentSequence[i]] + "</size></color>  ";
            }
            else
            {
                // 아직 입력 안한 키는 흰색으로
                result += keyIcons[currentSequence[i]] + "  ";
            }
        }
        return result;
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

    void ShowQTE()
    {
        if (qtePanel != null)
        {
            qtePanel.SetActive(true);
        }

        UpdateSequenceUI();
        UpdateDetectionCountUI();
        UpdateTimerUI();
    }

    void UpdateSequenceUI()
    {
        if (sequenceText != null)
        {
            sequenceText.text = GetSequenceString();
        }
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
            Time.timeScale = 1f;
        }

        UnfreezePlayer();
    }

    void UpdateTimerUI()
    {
        if (timerBar != null)
        {
            timerBar.fillAmount = timeRemaining / qteTime;

            if (timeRemaining / qteTime > 0.5f)
            {
                timerBar.color = Color.green;
            }
            else if (timeRemaining / qteTime > 0.25f)
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

    public bool IsQTEActive() 
    {
        return qteActive;
    }
}