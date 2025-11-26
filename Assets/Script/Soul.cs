using UnityEditor.EditorTools;
using UnityEngine;

public enum SoulType
{
    Target,    // Ã£¾Æ¾ß ÇÏ´Â ¿µÈ¥ 
    Normal     // ÀÏ¹Ý ¿µÈ¥µé
}

public class Soul : MonoBehaviour
{
    [Header("¿µÈ¥ ¼³Á¤")]
    public SoulType soulType = SoulType.Normal;
    public string soulName = "¿µÈ¥";

    [Header("´ë»ç")]
    [TextArea(2, 5)]
    public string[] dialogues = {
        "[¿µÈ¥] ",
        "[¿µÈ¥] "
    };

    private bool isCollected = false;

    public bool IsCollected => isCollected;

    public void Collect()
    {
        isCollected = true;

        // SoulManager¿¡ ¾Ë¸²
        if (SoulManager.Instance != null)
        {
            SoulManager.Instance.CollectSoul(this);
        }
    }

    public string[] GetDialogues()
    {
        return dialogues;
    }

    public bool IsTargetSoul()
    {
        return soulType == SoulType.Target;
    }

}