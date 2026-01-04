using System.Linq;
using UnityEngine;

public class Poison : Effect
{
    [SerializeField] private int _damage;
    [SerializeField] private float _tickTime;
    [SerializeField] private float _curTickTime = 0;
    [SerializeField] private Effect _bigDamage;

    protected override void UseEffect(Entity entity)
    {
        if (_curTickTime <= 0)
        {
            TakeDamage(entity);
            _curTickTime = _tickTime;
        }
        _curTickTime -= Time.deltaTime;
    }

    private void TakeDamage(Entity entity)
    {
        entity.ChangeHp(_damage * -1);
    }

    protected override void BuffEffects(Entity entity)
    {
        if (entity.Effects.Any(ef => ef.Type == TypeEffect.Fire))
        {
            entity.AddEffect(_bigDamage);
        }
    }

}
