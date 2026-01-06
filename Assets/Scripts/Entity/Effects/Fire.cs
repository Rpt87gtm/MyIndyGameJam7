using System.Linq;
using UnityEngine;

public class Fire : Effect
{
    [SerializeField] private int _damage;
    [SerializeField] private Effect _bigDamage;
    public override void StartEffect(Entity entity)
    {
        base.StartEffect(entity);
        TakeDamage(entity);
    }

    private void TakeDamage(Entity entity)
    {
        entity.ChangeHp(_damage * -1);
    }

    protected override void BuffEffects(Entity entity)
    {
        if (entity.Effects.Any(ef => ef.Type == TypeEffect.Poison))
        {
            entity.AddEffect(_bigDamage);
        }
    }
}