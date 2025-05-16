using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject defaultEnemyPrefab;
    public GameObject chunkEnemyPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 0.5f;
    public float spawnDistance = 10f;
    [Range(0f,1f)] public float chunkSpawnChance = 0.1f; // 10% de probabilidad

    public Transform player;

    private float timer;

    void Update()
    {
        if (!GameManager.Instance.HasSelectedPerk) return;
        if (player == null || PlayerStats.Instance == null || PlayerStats.Instance.isDead) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnDir = Random.insideUnitCircle.normalized;
        Vector2 spawnPos = (Vector2)player.position + spawnDir * spawnDistance;

        GameObject toSpawn = (Random.value <= chunkSpawnChance && chunkEnemyPrefab != null)
                                ? chunkEnemyPrefab
                                : defaultEnemyPrefab;

        Instantiate(toSpawn, spawnPos, Quaternion.identity);
    }
}
