using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Fire : Effect
{
    [SerializeField] private int _damage;
    [SerializeField] private float _tickTime;
    [SerializeField] private float _curTickTime = 0;

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
}