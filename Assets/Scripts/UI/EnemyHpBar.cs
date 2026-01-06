using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{

    public Entity CurEntity;
    private Slider _slider;
    [SerializeField] private Vector2 _offset = new Vector3(0, 0.5f); 


    private void SetHpInCanvas(int hp)
    {
        _slider.value = hp;
    }

    private void Start()
    {
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

    private void FixedUpdate()
    {
        Vector3 vector = _offset;
        if (CurEntity == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = CurEntity.transform.position + vector;
    }
}
