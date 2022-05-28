using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Properties")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnTimer = 4;
    [SerializeField] float spawnDelay = 2;    

    void Awake()
    {
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnTimer);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new(Random.Range(-20, 20), Random.Range(1, 5), Random.Range(-20, 20));

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, null);
        enemy.name = "Evil Spirit";
    }
}
