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
        BulletType[] types = (BulletType[]) Enum.GetValues(typeof(BulletType));
        foreach (BulletType type in types)
        {
            _bullets[type] = 0;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<BulletItem>(out BulletItem bulletItem))
        {
            GrabBullet(bulletItem);
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

