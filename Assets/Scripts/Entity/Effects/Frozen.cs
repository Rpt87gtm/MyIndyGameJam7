using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Frozen : Effect
{
    [SerializeField] private float _slow;
    protected override void UseEffect(Entity entity)
    {
        ChangeSpeed(entity);
    }



    public void ChangeSpeed(Entity entity)
    {
        entity.ChangeSpeed(_slow * -1);
    }
}
