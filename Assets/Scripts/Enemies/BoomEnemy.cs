using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Entity), typeof(NavMeshAgent), typeof(BoxCollider2D))]
public class BoomEnemy : MonoBehaviour
{


    [SerializeField] int _baseDmg = 50;
    private NavMeshAgent _agent;
    private Entity _entity;
    private Player _player;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _entity = GetComponent<Entity>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _player = FindAnyObjectByType<Player>();
    }
    private void FixedUpdate()
    {
        if (!_entity.IsAlive())
        {
            Dead();
            return;
        }

        if (_entity.IsFreeze)
        {
            _agent.SetDestination(transform.position);
            return;
        }
        
        _agent.speed = _entity.Speed;
        _agent.SetDestination(_player.transform.position);
    }

    private void Dead()
    {
        Destroy(gameObject);
    }

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
        _entity.ChangeHp(-10000);
    }
}
