using UnityEditor.EditorTools;
using UnityEngine;

public enum SoulType
{
    Target,    // 찾아야 하는 영혼 (가게 주인)
    Normal     // 일반 영혼들
}

public class Soul : MonoBehaviour
{
    [Header("영혼 설정")]
    public SoulType soulType = SoulType.Normal;
    public string soulName = "영혼";

    [Header("대사")]
    [TextArea(2, 5)]
    public string[] dialogues = {
        "[영혼] 고마워요...",
        "[영혼] 이제 편히 쉴 수 있겠어요."
    };

    private bool isCollected = false;

    public bool IsCollected => isCollected;

    public void Collect()
    {
        isCollected = true;

        // SoulManager에 알림
        if (SoulManager.Instance != null)
        {
            SoulManager.Instance.CollectSoul(this);
        }
    }

    public string[] GetDialogues()
    {
        return dialogues;
    }
}