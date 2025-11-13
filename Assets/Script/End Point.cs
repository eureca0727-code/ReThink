using UnityEngine;
using UnityEngine.UI;  // UI 사용을 위해 필요!

public class EndPoint : MonoBehaviour
{
    [Header("클리어 UI")]
    public GameObject clearUI;  // 클리어 문구 UI

    void Start()
    {
        // 시작할 때 클리어 UI 숨기기
        if (clearUI != null)
        {
            clearUI.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 닿았는지 확인
        if (other.CompareTag("Player"))
        {
            ShowClear();
        }
    }

    void ShowClear()
    {
        Debug.Log(" 클리어!");

        // 클리어 UI 표시
        if (clearUI != null)
        {
            clearUI.SetActive(true);
        }

        // 게임 일시정지 (선택사항)
        Time.timeScale = 0f;
    }
}