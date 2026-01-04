using System.Collections.Generic;
using bullets;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public BulletsList bulletsList;
    public Transform spawnPosition;

    public float shootCooldown = 0.2f;
    private Collider2D shooterCollider;

    private Coroutine shootCoro;


    void Start()
    {
        shooterCollider = GetComponent<Collider2D>();
    }
    private void ShootBullet(Vector2 direction, BulletType bulletType)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        var bullet = Instantiate(bulletsList.GetBulletByType(bulletType).prefab, spawnPosition.position, rotation);
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

        bulletComponent.Shoot(direction);
    }

    public bool TryShoot(Vector2 target, List<BulletType> bulletTypes)
    {
        if (shootCoro != null)
        {
            Debug.LogWarning("We shooting now");
            return false;
        }
        Vector2 position2D = new Vector2(spawnPosition.position.x, spawnPosition.position.y);
        var moveDirection = (target - position2D).normalized;
        shootCoro = StartCoroutine(ShootBullets(moveDirection, bulletTypes));
        return true;
    }

    private System.Collections.IEnumerator ShootBullets(Vector2 direction, List<BulletType> bulletTypes)
    {
        foreach (var type in bulletTypes)
        {
            ShootBullet(direction, type);
            yield return new WaitForSeconds(shootCooldown);
        }
        StopShooting();
    }
    public void StopShooting()
    {
        if (shootCoro != null)
        {
            StopCoroutine(shootCoro);
            shootCoro = null;
        }
    }
}
