using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    public static bool hasReachedEnd = false; // 엔딩 지점에 도달했는지 여부
    public Transform returnPosition; // 돌아갈 위치 (시작 지점)

    [Header("Visual Feedback")]
    public GameObject completionEffect; // 엔딩 터치 시 이펙트 (선택사항)

    private void Start()
    {
        hasReachedEnd = false; // 게임 시작 시 초기화
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasReachedEnd)
        {
            // 엔딩 지점 도달
            hasReachedEnd = true;
            Debug.Log("엔딩 지점 도달! 시작 지점으로 돌아가세요.");

            // 시각적 피드백 (선택사항)
            if (completionEffect != null)
            {
                Instantiate(completionEffect, transform.position, Quaternion.identity);
            }

            // 플레이어를 시작 위치로 이동
            if (returnPosition != null)
            {
                other.transform.position = returnPosition.position;
            }
        }
    }
}