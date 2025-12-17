using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [Header("소환 설정")]
    public GameObject chasingEnemyPrefab;  // 추적 적 프리팹
    public Transform[] spawnPoints;        // 소환 위치들
    public float spawnRadius = 15f;        // 플레이어 주변 소환 반경

    [Header("소환 현황")]
    public int enemiesKilled = 0;          // 죽인 적 수 (다른 스크립트에서 설정)

    private List<GameObject> spawnedChasingEnemies = new List<GameObject>();
    private Transform playerTransform;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // 플레이어 찾기
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    // 죽인 적 수만큼 추적 적 소환
    public void SpawnChasingEnemies()
    {
        if (chasingEnemyPrefab == null)
        {
            Debug.LogError("추적 적 프리팹이 설정되지 않았습니다!");
            return;
        }

        if (playerTransform == null)
        {
            Debug.LogError("플레이어를 찾을 수 없습니다!");
            return;
        }

        Debug.Log($"추적 적 {enemiesKilled}마리 소환 시작!");

        for (int i = 0; i < enemiesKilled; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            SpawnChasingEnemy(spawnPosition);
        }
    }

    // 랜덤 소환 위치 계산
    private Vector2 GetRandomSpawnPosition()
    {
        // 소환 포인트가 있으면 그 중 하나 사용
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            return randomPoint.position;
        }

        // 없으면 플레이어 주변 랜덤 위치
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPos = (Vector2)playerTransform.position + randomDirection * spawnRadius;

        return spawnPos;
    }

    // 추적 적 소환
    private void SpawnChasingEnemy(Vector2 position)
    {
        GameObject enemy = Instantiate(chasingEnemyPrefab, position, Quaternion.identity);

        // 추적 컴포넌트 설정
        ChasingEnemy chasingComponent = enemy.GetComponent<ChasingEnemy>();
        if (chasingComponent != null)
        {
            chasingComponent.SetTarget(playerTransform);
        }

        spawnedChasingEnemies.Add(enemy);

        Debug.Log($"추적 적 소환: {enemy.name} at {position}");
    }

    // 적을 죽였을 때 호출 (다른 스크립트에서 사용)
    public void RegisterEnemyKill()
    {
        enemiesKilled++;
        Debug.Log($"적 처치! 총 {enemiesKilled}마리");
    }

    // 소환된 적들 제거
    public void ClearChasingEnemies()
    {
        foreach (GameObject enemy in spawnedChasingEnemies)
        {
            if (enemy != null)
                Destroy(enemy);
        }

        spawnedChasingEnemies.Clear();
        Debug.Log("모든 추적 적 제거됨");
    }
}