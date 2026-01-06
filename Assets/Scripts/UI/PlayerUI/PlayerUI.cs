using bullets;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public MoveUpDown BarabanUpper;
    public BulletsUI bulletsUI;
    public Baraban baraban;
    public PlayerHpBar Bar;
    private Reload _reload;
    private List<BulletType> chosenBullet = new();
    private Dictionary<BulletType, int> currentBullets;

    void Start()
    {
        BarabanUpper.MoveDown(true);
    }
    public void Init(Reload reload)
    {
        _reload = reload;
    }

    public void StartReload(Dictionary<BulletType, int> bullets)
    {
        Debug.Log("Start Reload");
        chosenBullet.Clear();
        BarabanUpper.MoveUp();
        currentBullets = bullets;
        bulletsUI.ShowBullets(bullets, BulletChosen);
    }
    public void OnShoot(BulletType bulletType)
    {
        baraban.RemoveBullet();
    }
    public void BulletChosen(BulletType bulletType)
    {
        chosenBullet.Add(bulletType);
        baraban.AddBullet(bulletType);
        currentBullets[bulletType]--;
        bulletsUI.ShowBullets(currentBullets, BulletChosen);
        _reload.ReloadCallback(bulletType);
    }

    public void StopReload()
    {
        BarabanUpper.MoveDown();
        bulletsUI.hideBullets();
    }

    public void SubscribeBar(Entity entity)
    {
        Bar.CurEntity = entity;
        Bar.Subs();
    }
}
