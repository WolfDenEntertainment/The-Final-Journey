using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Properties")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnTimer = 4;
    [SerializeField] float initialDelay = 5;
    [SerializeField] int maxEnemies = 10;

    List<GameObject> enemiesInScene = new();

    void Start()
    {
        StartSpawning();
    }

    void Update()
    {
        if (enemiesInScene.Count < maxEnemies)
            CancelInvoke();

        if (enemiesInScene.Count == 0)
            StartSpawning();

    }

    void StartSpawning()
    {
        InvokeRepeating("SpawnEnemy", initialDelay, spawnTimer);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new(Random.Range(-30, 30), Random.Range(1, 5), Random.Range(-30, 30));

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, null);
        enemy.name = "Evil Spirit";
        enemiesInScene.Add(enemy);
    }
}
