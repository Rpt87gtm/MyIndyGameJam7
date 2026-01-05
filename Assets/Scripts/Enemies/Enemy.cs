using System.Linq;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Entity), typeof(NavMeshAgent), typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _agrRadius = 5;
    [SerializeField] GameObject _dropItem;
    [SerializeField] int _countDrop = 0;
    [SerializeField] float _forceDrop = 0.5f;
    private NavMeshAgent _agent;
    private Entity _entity;
    private Player _player;

    public float ArgRadius => _agrRadius;
    public NavMeshAgent Agent => _agent;
    public Entity CurEntity => _entity;
    public Player CurPlayer => _player;


    public bool IsIdle => _entity.IsIdle;

    protected virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _entity = GetComponent<Entity>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _player = FindAnyObjectByType<Player>();
    }



    protected virtual void FixedUpdate()
    {
        ChangeIdleBecausePlayer();
        ChangeIdleNearestEnemy();
        if (!_entity.IsAlive())
        {
            Dead();
            return;
        }
        _agent.speed = _entity.Speed;
        CheckFreeze();
        Movement();
    }


    protected virtual void Dead()
    {
        if (_dropItem != null && _countDrop > 0)
            BulletDrop.Drop(_dropItem, _countDrop, _forceDrop, transform.position);
        Destroy(gameObject);
    }

    protected virtual void Movement()
    {
        if (IsIdle)
            return;
        _agent.speed = _entity.Speed;
        _agent.SetDestination(_player.transform.position);
    }



    protected virtual void ChangeIdleBecausePlayer()
    {
        if (!IsIdle)
            return;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ArgRadius, LayerMask.GetMask("Entity"));
        if (colliders.Length > 1)
        {
            if (colliders.Any(col => col.gameObject.TryGetComponent<Player>(out Player player)))
            {
                _entity.SetIdle(false);
            }

        }
    }

    void OnDrawGizmos()
    {
        // Draws every frame in Scene view
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ArgRadius);
    }

    protected virtual void ChangeIdleNearestEnemy()
    {
        if (IsIdle)
            return;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ArgRadius);
        if (colliders.Length <= 1)
            return;
        var otherEntity = colliders.Where(col => col.gameObject.TryGetComponent(out Entity entity)).Select(col => col.gameObject.GetComponent<Entity>());
        foreach ( var ent in otherEntity)
        {
            ent.SetIdle(false);
        }

    }

    protected void CheckFreeze()
    {
        if (_entity.IsFreeze)
        {
            _agent.SetDestination(transform.position);
            return;
        }
    }
}
