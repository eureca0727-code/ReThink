using UnityEngine;

public class SoulManager : MonoBehaviour
{
    public static SoulManager Instance;

    [Header("수집 현황")]
    public int normalSoulsCollected = 0;
    public bool targetSoulCollected = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CollectSoul(Soul soul)
    {
        if (soul.soulType == SoulType.Target)
        {
            targetSoulCollected = true;
            Debug.Log("목표 영혼 구출 완료!");

            // 여기서 탈출 시퀀스 시작할 수 있음
            // escapeSequence.StartEscape();
        }
        else if (soul.soulType == SoulType.Normal)
        {
            normalSoulsCollected++;
            Debug.Log($"일반 영혼 수집: {normalSoulsCollected}개");
        }
    }
}