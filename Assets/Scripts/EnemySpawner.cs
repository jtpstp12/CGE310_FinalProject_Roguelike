using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private float initialSpawnInterval = 3f;
    private float spawnInterval;
    private float timePassed = 0f;

    [SerializeField] private Transform player;

    private int enemyLevel = 1;

    private void Start()
    {
        spawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnEnemyRoutine());
    }

    private void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= 60f)
        {
            timePassed = 0f;
            enemyLevel++;

            spawnInterval = Mathf.Max(1f, initialSpawnInterval * Mathf.Pow(0.95f, enemyLevel));

            Debug.Log($"Enemy level {enemyLevel} | Spawn interval = {spawnInterval:F2}s");
        }
    }

    // 👇 ย้ายออกมานอก Update()
    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector2 spawnPosition = (Vector2)player.position + Random.insideUnitCircle * spawnRadius;
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            newEnemy.GetComponent<EnemyAI>()?.SetLevel(enemyLevel);
            newEnemy.GetComponent<EnemyHealth>()?.SetLevel(enemyLevel);
        }
    }
}
