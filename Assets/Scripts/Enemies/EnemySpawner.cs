using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;
    public Transform player;

    private float timer;

    void Update()
    {
        if (player == null) return;

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

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}