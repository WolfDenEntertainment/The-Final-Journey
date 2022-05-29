using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Transform[] itemSpawnPoints;
    [SerializeField] Transform[] itemsToSpawn;
    
    void Awake()
    {
        if (itemSpawnPoints == null)
            itemSpawnPoints = GetComponentsInChildren<Transform>();

        SpawnItems();
    }

    void SpawnItems()
    {
        int index;

        if (itemsToSpawn != null)
        {
            for (int i = 0; i < itemsToSpawn.Length; i++)
            {
                index = Random.Range(0, itemSpawnPoints.Length);

                if (itemSpawnPoints[index] != null)
                {
                    Transform item = Instantiate(itemsToSpawn[i], itemSpawnPoints[index].position, Quaternion.identity);
                    itemSpawnPoints[index] = null;
                    item.name = itemsToSpawn[i].name;
                }
            }
        }
    }
}
