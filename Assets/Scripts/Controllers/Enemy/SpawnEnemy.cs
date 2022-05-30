using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] Transform[] enemySpawnPoints;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float initialDelay = 5;
    [SerializeField] float spawnDelay = 15;

    void Start()
    {
        InvokeRepeating("Spawn", initialDelay, spawnDelay);
    }

    void Spawn()
    {
        int index = Random.Range(0, enemySpawnPoints.Length);
        Vector3 spawnPosition = enemySpawnPoints[index].position;

        Debug.Log("Spawn Position:  " + spawnPosition.ToString());

        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        enemy.name = "Evil Spirit";
    }
}
