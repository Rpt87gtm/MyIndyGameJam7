using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class BoomEnemy : Enemy
{


    [SerializeField] int _baseDmg = 50;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Entity>(out Entity entity))
        {
            Boom(entity);
        }
    }

    private void Boom(Entity entity)
    {
        entity.ChangeHp(_baseDmg * -1);
        CurEntity.ChangeHp(-10000);
    }

}
