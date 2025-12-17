using UnityEngine;
using UnityEngine.Events;

public class SoulManager : MonoBehaviour
{
    public static SoulManager Instance;

    [Header("수집 현황")]
    public int normalSoulsCollected = 0;
    public bool targetSoulCollected = false;

    [Header("대시 해금 설정")]
    public int requiredNormalSouls = 3; // 대시 활성화에 필요한 일반 영혼 수
    public bool dashUnlocked = false;

    [Header("이벤트")]
    public UnityEvent onDashUnlocked;
    public UnityEvent onTargetSoulCollected;
    public UnityEvent onNormalSoulCollected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지 (선택사항)
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 이벤트 초기화
        if (onDashUnlocked == null)
            onDashUnlocked = new UnityEvent();
        if (onTargetSoulCollected == null)
            onTargetSoulCollected = new UnityEvent();
        if (onNormalSoulCollected == null)
            onNormalSoulCollected = new UnityEvent();
    }

    public void CollectSoul(Soul soul)
    {
        if (soul.soulType == SoulType.Target)
        {
            targetSoulCollected = true;
            Debug.Log("목표 영혼 구출 완료!");

            // 이벤트 발동
            onTargetSoulCollected?.Invoke();

            // 여기서 탈출 시퀀스 시작할 수 있음
            // escapeSequence.StartEscape();
        }
        else if (soul.soulType == SoulType.Normal)
        {
            normalSoulsCollected++;
            Debug.Log($"일반 영혼 수집: {normalSoulsCollected}개");

            // 이벤트 발동
            onNormalSoulCollected?.Invoke();

            // 대시 해금 체크
            CheckDashUnlock();
        }
    }

    private void CheckDashUnlock()
    {
        // 아직 해금되지 않았고, 필요한 개수를 달성했으면
        if (!dashUnlocked && normalSoulsCollected >= requiredNormalSouls)
        {
            dashUnlocked = true;
            Debug.Log($"<color=yellow>대시 능력 해금! ({normalSoulsCollected}/{requiredNormalSouls})</color>");

            // 대시 해금 이벤트 발동
            onDashUnlocked?.Invoke();

        }
    }

    // 대시가 해금되었는지 확인하는 메서드
    public bool IsDashUnlocked()
    {
        return dashUnlocked;
    }

    // 현재 수집 상태를 반환하는 메서드
    public string GetCollectionStatus()
    {
        return $"일반 영혼: {normalSoulsCollected}개 | 목표 영혼: {(targetSoulCollected ? "구출 완료" : "미구출")}";
    }

    // 대시 해금까지 남은 영혼 수
    public int GetSoulsUntilDash()
    {
        if (dashUnlocked)
            return 0;
        return Mathf.Max(0, requiredNormalSouls - normalSoulsCollected);
    }

    // 진행률 (0.0 ~ 1.0)
    public float GetDashUnlockProgress()
    {
        return Mathf.Clamp01((float)normalSoulsCollected / requiredNormalSouls);
    }

    // 게임 상태 초기화 (재시작 시 사용)
    public void ResetProgress()
    {
        normalSoulsCollected = 0;
        targetSoulCollected = false;
        dashUnlocked = false;
        Debug.Log("SoulManager 진행 상태 초기화");
    }
}