using System;
using System.Collections.Generic;
using bullets;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Inventory : MonoBehaviour
{

    private Dictionary<BulletType, int> _bullets = new Dictionary<BulletType, int>();

    private void Start()
    {
        BulletType[] types = (BulletType[])Enum.GetValues(typeof(BulletType));
        foreach (BulletType type in types)
        {
            _bullets[type] = 0;
        }
    }

    public Dictionary<BulletType, int> GetBulletsCount()
    {
        return new(_bullets);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<BulletItem>(out BulletItem bulletItem))
        {
            GrabBullet(bulletItem);
        }
    }

    public void UseBullet(BulletType bullet)
    {
        if (bullet == BulletType.Normal)
            return;
        if (!_bullets.ContainsKey(bullet))
            Debug.LogError("Use unlocked bullet");
        else
        {
            _bullets[bullet]--;
        }
    }

    private void GrabBullet(BulletItem bullet)
    {
        if (bullet.Type == BulletType.Normal)
            return;
        if (!_bullets.ContainsKey(bullet.Type))
            _bullets.Add(bullet.Type, 0);
        _bullets[bullet.Type]++;

        bullet.Dest();
        Debug.Log(_bullets[bullet.Type]);
    }
}

