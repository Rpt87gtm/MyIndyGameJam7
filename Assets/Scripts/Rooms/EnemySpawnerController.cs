using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemies;

    public void SpawnEnemies()
    {
        EnemySpawner[] enemySpawners = GetComponentsInChildren<EnemySpawner>();
        foreach(EnemySpawner enemySpawner in enemySpawners)
        {
            _enemies.Add(enemySpawner.SpawnEnemy());
        }
    }


    [ContextMenu(nameof(RespawnEnemies))]
    public void RespawnEnemies()
    {
        foreach (var enemy in _enemies)
        {
            if (enemy != null)
                Destroy(enemy);
            _enemies = new();
        }
        SpawnEnemies();

    }
}
