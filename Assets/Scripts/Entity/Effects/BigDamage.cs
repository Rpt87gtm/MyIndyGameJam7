using UnityEngine;

public class BigDamage : Effect
{
    [SerializeField] private int _firstDamage;
    [SerializeField] private int _tickDamage;
    [SerializeField] private float _tickTime;
    [SerializeField] private float _curTickTime = 0;

    public override void StartEffect(Entity entity)
    {
        base.StartEffect(entity);
        _curTickTime = _tickTime;
        TakeDamage(entity,_firstDamage);
    }

    protected override void UseEffect(Entity entity)
    {
        if (_curTickTime <= 0)
        {
            TakeDamage(entity, _tickDamage);
            _curTickTime = _tickTime;
        }
        _curTickTime -= Time.deltaTime;
    }

    private void TakeDamage(Entity entity, int damage)
    {
        entity.ChangeHp(damage * -1);
    }

}
