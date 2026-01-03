using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class Effect: MonoBehaviour
{
    
    [SerializeField] private TypeEffect _type;
    [SerializeField] private List<TypeEffect> _dropableEffects = new List<TypeEffect>();
    public TypeEffect Type => _type;
    public IReadOnlyList<TypeEffect> DropableEffects => _dropableEffects;


    public virtual void UseEffect(Entity entity)
    {
    }


    public virtual void StartEffect(Entity entity)
    {
        BuffEffects();
        DropEffects(entity);
    }

    protected virtual void DropEffects(Entity entity)
    {
        var effects = entity.Effects.Where(ef => !_dropableEffects.Contains(ef.Type)).ToList();
        foreach (var effect in effects)
        {
            Debug.Log(effect);
        }
        entity.Effects = effects;
    }

    protected virtual void BuffEffects()
    {
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