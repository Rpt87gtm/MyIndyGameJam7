using bullets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulletButton : MonoBehaviour
{
    public BulletType bulletType;
    public TextMeshProUGUI countText;
    private BulletsUI _bulletsUI;
    private Button _button;
    void Awake()
    {
        _button = GetComponent<Button>();
    }
    public void Init(BulletsUI bulletsUI)
    {
        _bulletsUI = bulletsUI;
        _button.onClick.AddListener(BulletPressed);
    }

    public void BulletPressed()
    {
        _bulletsUI.BulletPressed(this);
    }
    public void ShowCount(int count)
    {
        if (bulletType == BulletType.Normal)
        {
            countText.SetText("\u221E");
        }
        _button.interactable = true;
        countText.SetText($"x{count}");
    }
    public void ShowLocked()
    {
        if (bulletType == BulletType.Normal) return;
        _button.interactable = false;
    }
}
