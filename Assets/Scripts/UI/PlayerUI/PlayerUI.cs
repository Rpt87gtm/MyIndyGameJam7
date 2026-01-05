using System;
using System.Collections.Generic;
using bullets;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public MoveUpDown BarabanUpper;
    public BulletsUI bulletsUI;
    private Action<List<BulletType>> _callback;
    private List<BulletType> chosenBullet = new();
    private Dictionary<BulletType, int> currentBullets;

    void Start()
    {
        BarabanUpper.MoveDown(true);
    }
    public void Init(Action<List<BulletType>> callback)
    {
        _callback = callback;
    }

    public void StartReload(Dictionary<BulletType, int> bullets)
    {
        BarabanUpper.MoveUp();
        currentBullets = bullets;
        bulletsUI.ShowBullets(bullets, BulletChosen);
    }

    public void BulletChosen(BulletType bulletType)
    {
        chosenBullet.Add(bulletType);
        if (chosenBullet.Count > 5)
        {
            Debug.Log("ammo full");
            _callback(chosenBullet);
            return;
        }
        currentBullets[bulletType]--;
        bulletsUI.ShowBullets(currentBullets, BulletChosen);
    }

    public void StopReload()
    {
        BarabanUpper.MoveDown();
        bulletsUI.hideBullets();
        List<BulletType> bullets = new() { BulletType.Electric, BulletType.Normal };
        chosenBullet.Clear();
    }
}
