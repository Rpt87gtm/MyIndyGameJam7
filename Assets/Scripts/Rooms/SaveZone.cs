using UnityEngine;

public class SaveZone : MonoBehaviour
{
    [SerializeField] private EnemySpawnerController _enemySpawnerController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            player.Spawner = this;
            Debug.Log("Zone");
        }
    }
    public void RespawnEnemies()
    {
        _enemySpawnerController.RespawnEnemies();
    }
}
