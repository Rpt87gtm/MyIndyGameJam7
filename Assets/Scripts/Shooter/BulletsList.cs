using bullets;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullets list", menuName = "Bullets/bullets list")]
public class BulletsList : ScriptableObject
{
    public List<BulletConfig> bullets = new();

    public BulletConfig GetBulletByType(BulletType type)
    {

        return bullets.Find(bullet => bullet.bulletType == type);
    }
    public void AddBullet(BulletConfig bulletConfig)
    {
        bullets.Add(bulletConfig);
    }

    public void RemoveBullet(BulletType bulletType)
    {
        foreach (var bullet in bullets)
        {
            if (bullet.bulletType == bulletType)
            {
                bullets.Remove(bullet);
            }
        }
    }
}
