using UnityEngine;

public class EnemyDialogue : MonoBehaviour
{
    [Header("이 적의 대사")]
    [TextArea(2, 5)]
    public string[] dialogues = {
        "[적] 죽이던지 살리던지 마음대로 해 ",
        "[나] ",
    };

    public string[] GetDialogues()
    {
        return dialogues;
    }
}
