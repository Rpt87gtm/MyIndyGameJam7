using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;

    public GameObject SpawnEnemy()
    {
        GameObject enemy = GameObject.Instantiate(_enemy, transform.position, Quaternion.identity);
        return enemy;
    }

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
