using bullets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulletButton : MonoBehaviour
{
    public BulletType bulletType;
    public TextMeshProUGUI countText;
    public Image bulletImage;

    public Sprite emptySlot;
    private BulletsUI _bulletsUI;
    private Button _button;
    private Sprite sprite;
    private Image backgroundImage;
    void Awake()
    {
        _button = GetComponent<Button>();
        backgroundImage = GetComponent<Image>();
        sprite = backgroundImage.sprite;
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
        else
        {
            if (count <= 0)
            {
                ShowLocked();
                return;
            }
            countText.SetText($"x{count}");
        }
        bulletImage.color = new Color(1f, 1f, 1f, 1f);
        backgroundImage.sprite = sprite;
        _button.interactable = true;
    }
    public void ShowLocked()
    {
        if (bulletType == BulletType.Normal) return;
        bulletImage.color = new Color(1f, 1f, 1f, 0f);
        countText.SetText("");
        backgroundImage.sprite = emptySlot;
        _button.interactable = false;
    }
}
