using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


[RequireComponent(typeof(SpriteRenderer))]
public class Entity : MonoBehaviour
{
    [SerializeField] private EntityData _entityData;


    [SerializeField] private List<Effect> _effects = new List<Effect>();

    public IReadOnlyList<Effect> Effects => _effects;

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
        
        foreach (var effect in Effects)
        {
            effect.Tick(this);
        }
    }

    public void AddEffect(Effect effect)
    {
        GameObject createdEffectObj = Instantiate(effect.gameObject, transform.position, Quaternion.identity) as GameObject;
        createdEffectObj.transform.SetParent(transform, true);
        Effect createdEffect = createdEffectObj.GetComponent<Effect>();
        createdEffect.StartEffect(this);
        _effects.Add(createdEffect);
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
        if (!_entityData.isAlive())
            Dead();
    }

    public void InitEntity()
    {
        _entityData.HpSetDefault();
    }


    public void Dead()
    {
        Destroy(gameObject);
    }



}
