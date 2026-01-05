using System;
using System.Collections.Generic;
using bullets;
using UnityEngine;

public class BulletsUI : MonoBehaviour
{
    public MoveUpDown moveUpDown;
    public List<BulletButton> bulletButtons;
    private Action<BulletType> _callback;
    void Start()
    {
        foreach (var bullet in bulletButtons)
        {
            bullet.Init(this);
        }
        moveUpDown.MoveDown(true);
    }
    public void BulletPressed(BulletButton bulletButton)
    {
        _callback(bulletButton.bulletType);
    }


    public void ShowBullets(Dictionary<BulletType, int> bullets, Action<BulletType> callback)
    {
        _callback = callback;
        foreach (var bullet in bulletButtons)
        {
            if (!bullets.ContainsKey(bullet.bulletType))
            {
                bullet.ShowLocked();
            }
            else
            {
                bullet.ShowCount(bullets[bullet.bulletType]);
            }
        }
        Debug.Log("move bullets up");
        moveUpDown.MoveUp();
    }

    public void hideBullets()
    {
        Debug.Log("hide bullets");
        moveUpDown.MoveDown();
    }
}
