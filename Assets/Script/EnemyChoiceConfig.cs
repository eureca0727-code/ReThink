using UnityEngine;

public class EnemyChoiceConfig : MonoBehaviour
{
    [Header("선택지 설정")]
    public bool canBecomeAlly = true;  // 살리기 가능 여부
    public bool canBeKilled = true;    // 죽이기 가능 여부

    [Header("선택지 비활성 메시지 (선택 불가시 표시)")]
    [TextArea(1, 3)]
    public string allyDisabledMessage = "이 적은 설득할 수 없다.";
    [TextArea(1, 3)]
    public string killDisabledMessage = "죽일 수 없다.";
}