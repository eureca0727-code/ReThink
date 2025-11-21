using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPoint : MonoBehaviour
{
    [Header("Game Clear Settings")]
    public string nextSceneName = ""; // 다음 씬 이름 (비어있으면 현재 씬 재시작)
    public float clearDelay = 1f; // 게임 클리어 후 대기 시간

    [Header("Visual Feedback")]
    public GameObject clearEffect; // 게임 클리어 이펙트

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && EndPoint.hasReachedEnd)
        {
            // 게임 클리어!
            Debug.Log("게임 클리어!");

            // 시각적 피드백
            if (clearEffect != null)
            {
                Instantiate(clearEffect, transform.position, Quaternion.identity);
            }

            // 게임 종료 처리
            Invoke("CompleteGame", clearDelay);
        }
    }

    private void CompleteGame()
    {
        EndPoint.hasReachedEnd = false; // 플래그 초기화

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 재시작
        }
    }
}