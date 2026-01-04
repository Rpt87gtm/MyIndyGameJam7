using UnityEngine;

public class Stun : Effect
{
    protected override void UseEffect(Entity entity)
    {
        Freeze(entity);
    }

    private void Freeze(Entity entity)
    {
        entity.SetFreeze(true);
    }
}

