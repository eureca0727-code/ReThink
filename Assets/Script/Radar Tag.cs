using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyRadar : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어와 충돌했는지 확인
        if (other.CompareTag("Player"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");

        // 현재 씬 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // 또는 게임 정지
        // Time.timeScale = 0;
    }
}