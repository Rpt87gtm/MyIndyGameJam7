using UnityEngine;

public class SaveZone : MonoBehaviour
{
    [SerializeField] private EnemySpawnerController _enemySpawnerController;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("Player in collider");
        }
    }
    public void RespawnEnemies()
    {
        _enemySpawnerController.RespawnEnemies();
    }
}
