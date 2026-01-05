using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


[RequireComponent(typeof(SpriteRenderer))]
public class Entity : MonoBehaviour
{
    [SerializeField] private EntityData _entityData;


    [SerializeField] private List<Effect> _effects = new List<Effect>();

    [SerializeField] private bool _isFreeze = false;
    [SerializeField] private bool _isIdle = true;

    public IReadOnlyList<Effect> Effects => _effects;
    public bool IsFreeze => _isFreeze;
    public bool IsIdle => _isIdle;


    public float Speed => _entityData.CurrentSpeed;

    private Color _defaultColor;
    private SpriteRenderer _spriteRenderer;

  



    public void Start()
    {
        InitEntity();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
    }

    public void FixedUpdate()
    {
        _entityData.SetDefaultSpeed();
        _spriteRenderer.color = _defaultColor;
        SetFreeze(false);
        
        foreach (var effect in Effects)
        {
            effect.Tick(this);
        }
    }

    public void AddEffect(Effect effect)
    {
        if (_entityData.EffectResists.Contains(effect.Type))
        {
            return;
        }


        GameObject createdEffectObj = Instantiate(effect.gameObject, transform.position, Quaternion.identity) as GameObject;
        createdEffectObj.transform.SetParent(transform, true);
        Effect createdEffect = createdEffectObj.GetComponent<Effect>();
        createdEffect.StartEffect(this);
    }

    public void ChangeSpeed(float speed)
    {
        _entityData.ChangeSpeed(speed);
    }

    public void ChangeListEffects(List<Effect> effects)
    {
        _effects = effects;
    }

    public void ChangeHp(int hp)
    {
        _entityData.ChangeHp(hp);
    }

    public void InitEntity()
    {
        _entityData.HpSetDefault();
    }


    public void SetFreeze(bool isFreaze)
    {
        _isFreeze = isFreaze;
    }

    public bool IsAlive()
    {
        return _entityData.IsAlive();
    }

    public void SetIdle(bool isIdle)
    {
        _isIdle = isIdle;
    }





}
