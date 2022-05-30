using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Transform[] itemSpawnPoints;
    [SerializeField] Transform itemContainer;
    [SerializeField] Item[] itemsToSpawn;
    
    void Awake()
    {
        if (itemSpawnPoints == null)
            itemSpawnPoints = GetComponentsInChildren<Transform>();

        SpawnItems();
    }

    void SpawnItems()
    {
        int index;
        int[] indicesToCheck = new int[itemsToSpawn.Length];

        for (int i = 0; i < itemsToSpawn.Length; i++)
        {
            indicesToCheck[i] = -1;
        }

        if (itemsToSpawn != null)
        {
            for (int i = 0; i < itemsToSpawn.Length; i++)
            {
                index = Random.Range(0, itemSpawnPoints.Length);

                if (indicesToCheck[i] == -1)
                {
                    GameObject item = Instantiate(itemsToSpawn[i].ItemObject, itemSpawnPoints[index].position, Quaternion.identity, itemContainer); ;
                    item.name = itemsToSpawn[i].ItemObject.name;
                    indicesToCheck[i] = index;
                }
            }
        }
    }
}
