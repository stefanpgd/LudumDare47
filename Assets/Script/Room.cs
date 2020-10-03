using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> enemySpawners;

    public void EnableEnemySpawners() => enemySpawners.ForEach(enemySpawner => enemySpawner.EnableSpawning());
    public void DisableEnemySpawners() => enemySpawners.ForEach(enemySpawner => enemySpawner.DisableSpawning());
}
