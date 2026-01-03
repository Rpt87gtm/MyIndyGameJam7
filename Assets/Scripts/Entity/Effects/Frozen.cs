using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Frozen : Effect
{
    [SerializeField] private float _slow;
    [SerializeField] private Color _color;
    public override void UseEffect(Entity entity)
    {
        ChangeColor(entity);
    }


    public void ChangeColor(Entity entity)
    {
        entity.GetComponent<SpriteRenderer>().color = _color;
    }

    public void ChangeSpeed(Entity entity)
    {
        entity.EntityData.ChangeSpeed(_slow);
    }
}
