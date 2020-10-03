using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 649

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform roomParent;
    [SerializeField] private float spawnTimer;
    [SerializeField] private int spawnAmount;

    private float baseSpawnTimer;
    private int baseSpawnAmount;
    private bool isActive = false;

    private void Start()
    {
        baseSpawnTimer = spawnTimer;
        baseSpawnAmount = spawnAmount;
    }

    private void Update()
    {
        if(isActive)
        {
            if (spawnAmount > 0)
            {
                spawnTimer -= Time.deltaTime;

                if (spawnTimer < 0)
                {
                    spawnTimer = baseSpawnTimer;
                    spawnAmount--;

                    Instantiate(enemy, transform.position, Quaternion.identity, roomParent.transform);
                }
            }
        }
    }

    public void EnableSpawning()
    {
        spawnTimer = baseSpawnTimer;
        spawnAmount = baseSpawnAmount;
        isActive = true;
    }

    public void DisableSpawning() => isActive = false;
}
