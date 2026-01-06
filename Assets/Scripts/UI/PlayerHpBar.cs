using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public Entity CurEntity;
    private Slider _slider;


    private void SetHpInCanvas(int hp)
    {
        _slider.value = hp;
    }

    private void Start()
    {
        Entity CurEntity = FindFirstObjectByType<Player>().GetComponent<Entity>();
        _slider = GetComponent<Slider>();
        _slider.maxValue = CurEntity.GetMaxHp();
        _slider.value = CurEntity.GetCurHp();
        CurEntity.HpChanged += SetHpInCanvas;
    }

    private void OnDisable()
    {
        if (CurEntity != null)
            CurEntity.HpChanged -= SetHpInCanvas;
    }
}
