using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 649

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private float SpawnTimer;
    [SerializeField] private float SpawnAmount;
    private float StartSpawnTimer;

    private void Start()
    {
        StartSpawnTimer = SpawnTimer;
    }

    private void Update()
    {
        if (SpawnAmount > 0)
        {
            SpawnTimer -= Time.deltaTime;

            if (SpawnTimer < 0)
            {
                SpawnTimer = StartSpawnTimer;
                SpawnAmount--;

                Instantiate(Enemy, transform.position, Quaternion.identity);
            }
        }
    }
}
