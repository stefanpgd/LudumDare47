using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 649

public class EnemySpawner : MonoBehaviour
{
    [HideInInspector] public bool doneSpawning = false;

    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform roomParent;
    [SerializeField] private float spawnTimer;
    [SerializeField] private int spawnAmount;
    [HideInInspector] public Room room;

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
                spawnTimer -= 1f * Time.deltaTime;

                if (spawnTimer <= 0)
                {
                    spawnTimer = baseSpawnTimer;
                    spawnAmount--;

                    GameObject enem = Instantiate(enemy, transform.position, Quaternion.identity, roomParent.transform);
                    room.AddEnemy(enem.GetComponent<Enemy>());
                }
            }
            else
            {
                doneSpawning = true;
            }
        }
    }

    public void EnableSpawning()
    {
        spawnTimer = baseSpawnTimer;
        spawnAmount = baseSpawnAmount;
        isActive = true;
        doneSpawning = false;
    }

    public void DisableSpawning() => isActive = false;
}
