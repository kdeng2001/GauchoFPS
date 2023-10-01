using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnController : MonoBehaviour
{
    private ItemSpawner[] itemSpawners;
    // Start is called before the first frame update
    void Start()
    {
        itemSpawners = GetComponentsInChildren<ItemSpawner>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        foreach(ItemSpawner spawner in itemSpawners)
        {
            if(spawner.hasSpawned == false)
            {
                spawner.SpawnItem();
            }
        }
        
    }
}
