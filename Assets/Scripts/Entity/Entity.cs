using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


[RequireComponent(typeof(SpriteRenderer))]
public class Entity : MonoBehaviour
{
    public EntityData EntityData;


    [ReadOnly(true)]
    public List<Effect> Effects = new List<Effect>();

    private Color _defaultColor;
    private SpriteRenderer _spriteRenderer;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
    }

    public void FixedUpdate()
    {
        EntityData.SetDefaultSpeed();
        _spriteRenderer.color = _defaultColor;
        
        foreach (var effect in Effects)
        {
            effect.UseEffect(this);
        }
    }

    public void AddEffect(Effect effect)
    {
        effect.StartEffect(this);
        Effects.Add(effect);
    }


}
