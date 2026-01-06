using UnityEngine;

public class RoomsScript : MonoBehaviour
{
    public void Start()
    {
        EnemySpawnerController[] enemySpawnerControllers = GetComponentsInChildren<EnemySpawnerController>();
        foreach (EnemySpawnerController enemySpawnerController in enemySpawnerControllers)
        {
            enemySpawnerController.SpawnEnemies();
        }
    }
}
