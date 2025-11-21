using UnityEngine;

public class EnemyDialogue : MonoBehaviour
{
    [Header("이 적의 대사")]
    [TextArea(2, 5)]
    public string[] dialogues = {
        "[적] 넌 누구지?!",
        "[나] 조용히 해.",
        "[적] 제발 살려줘!",
        "[나] 선택하겠어."
    };

    public string[] GetDialogues()
    {
        return dialogues;
    }
}