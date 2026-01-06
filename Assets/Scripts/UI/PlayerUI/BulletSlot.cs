using UnityEngine.UI;
using UnityEngine;

public class BulletSlot : MonoBehaviour
{
    private BarabanBullet bullet;
    public Sprite emptyImage;
    private Image image;
    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetSlot(BarabanBullet new_bullet)
    {
        bullet = new_bullet;
        image.sprite = bullet.Image;
    }
    public void ClearSlot()
    {
        image.sprite = emptyImage;
    }
}
