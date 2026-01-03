using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject buttetPrefab;
    public Transform spawnPosition;
    private Collider2D shooterCollider;

    void Start()
    {
        shooterCollider = GetComponent<Collider2D>();
    }
    public void Shoot(Vector2 target)
    {
        var bullet = Instantiate(buttetPrefab, spawnPosition.position, spawnPosition.rotation);
        var bulletComponent = bullet.GetComponent<Bullet>();
        if (bulletComponent == null)
        {
            Debug.LogWarning("Instantiate not bullet");
            return;
        }

        var bulletCollider = bullet.GetComponent<Collider2D>();
        if (bulletCollider != null && shooterCollider != null)
        {
            Physics2D.IgnoreCollision(shooterCollider, bulletCollider, true);
        }
        else
        {
            Debug.LogError("No shooter or collider on bullet");
        }

        bulletComponent.SetTarget(target);
        bulletComponent.Shoot();
    }
}
