using System.Linq;
using UnityEngine;

public class Frozen : Effect
{
    [SerializeField] private float _slow;
    [SerializeField] private Effect _stun;
    protected override void UseEffect(Entity entity)
    {
        ChangeSpeed(entity);
    }



    public void ChangeSpeed(Entity entity)
    {
        entity.ChangeSpeed(_slow * -1);
    }

    protected override void BuffEffects(Entity entity)
    {
        if (entity.Effects.Any(ef => ef.Type == TypeEffect.Water))
        {
            entity.AddEffect(_stun);
        }
    }
}
