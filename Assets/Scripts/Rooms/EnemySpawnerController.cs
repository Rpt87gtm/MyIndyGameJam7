using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private Door _door;
    [SerializeField] private Door _door2;
    
    

    public void SpawnEnemies()
    {
        EnemySpawner[] enemySpawners = GetComponentsInChildren<EnemySpawner>();
        foreach(EnemySpawner enemySpawner in enemySpawners)
        {
            _enemies.Add(enemySpawner.SpawnEnemy());
        }
    }

    public void FixedUpdate()
    {
        if (_door != null && _enemies.All(en => en == null) && !_door.IsOpen)
        {
            Debug.Log("Open");
            _door.OpenDoor();
        }
        if (_door2 != null && _enemies.All(en => en == null) && !_door2.IsOpen)
        {
            Debug.Log("Open");
            _door2.OpenDoor();
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
