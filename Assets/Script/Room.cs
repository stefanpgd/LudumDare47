using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 649

public class Room : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> enemySpawners;
    [SerializeField] private List<DoorChecker> doors;

    private List<Enemy> activeEnemies = new List<Enemy>();
    private bool isActive = false;
    private bool hasUserCompletedRoom;

    private void Start() => enemySpawners.ForEach(spawner => spawner.room = this);

    private void Update()
    {
        if(isActive)
        {
            CheckActiveEnemies();

            if (hasUserCompletedRoom)
            {
                doors.ForEach(door => door.isInteractable = true);
            }
        }  
    }

    public void EnableRoom()
    {
        EnableEnemySpawners();
        doors.ForEach(door => door.isInteractable = false);
        isActive = true;
        hasUserCompletedRoom = false;
    }

    public void DisableRoom()
    {
        DisableEnemySpawners();
        isActive = false;
        activeEnemies.ForEach(enemy => Destroy(enemy.gameObject));
        activeEnemies.Clear();
    }

    public void AddEnemy(Enemy enemy) => activeEnemies.Add(enemy);

    private void CheckActiveEnemies()
    {
        bool spawningCompleted = true;
        bool areAllEnemiesDeath = true;

        foreach(EnemySpawner spawner in enemySpawners)
        {
            if(!spawner.doneSpawning)
            {
                spawningCompleted = false;
            }
        }

        if(spawningCompleted)
        {
            foreach(Enemy enemy in activeEnemies)
            {
                if(enemy.gameObject.activeInHierarchy)
                {
                    areAllEnemiesDeath = false;
                }
            }

            if(areAllEnemiesDeath)
            {
                hasUserCompletedRoom = true;
            }
        }
    }

    private void EnableEnemySpawners() => enemySpawners.ForEach(enemySpawner => enemySpawner.EnableSpawning());
    private void DisableEnemySpawners() => enemySpawners.ForEach(enemySpawner => enemySpawner.DisableSpawning());
}
