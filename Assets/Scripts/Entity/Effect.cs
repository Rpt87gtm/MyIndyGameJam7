using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Effect: MonoBehaviour
{
    
    [SerializeField] private TypeEffect _type;
    [SerializeField] private List<TypeEffect> _dropableEffects = new List<TypeEffect>();
    [SerializeField] private Color _color;
    [Range(0.1f, 10f)]
    [SerializeField] private float _duration;
    public TypeEffect Type => _type;
    public IReadOnlyList<TypeEffect> DropableEffects => _dropableEffects;
    public float Duration => _duration;

    [SerializeField] private float _curDuration;




    public void Tick(Entity entity)
    {
        ReduceDuration(entity);
        ChangeColor(entity);
        UseEffect(entity);
    }


    public virtual void StartEffect(Entity entity)
    {
        _curDuration = _duration;
        BuffEffects();
        DropEffects(entity);
    }

    public virtual void DestroyEffect()
    {
        Destroy(this.gameObject);
    }

    protected virtual void DropEffects(Entity entity)
    {
        var destroyableEffects = entity.Effects.Where(ef => _dropableEffects.Contains(ef.Type) || ef.Type == this.Type).ToList();
        foreach (var effect in destroyableEffects)
        {
            effect.DestroyEffect();
        }

        var effects = entity.Effects.Where(ef => !(_dropableEffects.Contains(ef.Type) || ef.Type == this.Type)).ToList();
        foreach (var effect in effects)
        {
            Debug.Log(effect);
        }
        entity.ChangeListEffects(effects);
    }

    protected virtual void BuffEffects()
    {
    }

    protected virtual void UseEffect(Entity entity)
    {
    }

    private void ChangeColor(Entity entity)
    {
        entity.GetComponent<SpriteRenderer>().color = _color;
    }



    private void ReduceDuration(Entity entity)
    {
        _curDuration -= Time.deltaTime;
        if ( _curDuration <= 0 )
        {
            var effects = entity.Effects.Where(ef => ef != this).ToList();
            entity.ChangeListEffects(effects);
            DestroyEffect();
        }
    }


}

public enum TypeEffect
{
    Fire,
    Water,
    Electric,
    Poison,
    Frozen
}