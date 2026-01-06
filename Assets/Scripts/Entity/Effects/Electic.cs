using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Electic : Effect
{
    [SerializeField] private int _damage;
    [SerializeField] private float _tickTime;
    [SerializeField] private float _curTickTime = 0;
    [SerializeField] private Effect _electric;

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
        if (entity.Effects.Any(ef => ef.Type == TypeEffect.Water))
        {
            List<Entity> entities = GameObject.FindObjectsByType<Entity>(FindObjectsSortMode.None).ToList();
            Debug.Log("zzz");
            foreach (var ent in entities)
            {
                if (ent.Effects.Any(ef => ef.Type == TypeEffect.Water))
                {
                    Debug.Log("www");
                    RemoveEffect(entity, TypeEffect.Water);
                    ent.AddEffect(_electric);
                }
            }
        }
    }
}
