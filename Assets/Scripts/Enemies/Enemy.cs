using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    [SerializeField] private float _agrRadius = 5;
    [SerializeField] private GameObject _dropItem;
    [SerializeField] private int _countDrop = 0;
    [SerializeField] private float _forceDrop = 0.5f;
    [SerializeField] private float _deadTime = 1f;
    [SerializeField] private EnemyHpBar _enemyHpBar;
     Animator _animator;
    private NavMeshAgent _agent;
    private Entity _entity;
    private Player _player;
    private bool _isDead = false;




 
    public float ArgRadius => _agrRadius;
    public NavMeshAgent Agent => _agent;
    public Entity CurEntity => _entity;
    public Player CurPlayer => _player;

    public bool IsDead => _isDead;

    public Animator Animator => _animator;

    public bool IsFreeze => _entity.IsFreeze;

    


    public bool IsIdle => _entity.IsIdle;

    protected virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _entity = GetComponent<Entity>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _player = FindAnyObjectByType<Player>();
        _animator = GetComponent<Animator>();
        GameObject enemyHpCanvas = FindFirstObjectByType<HpBarCanvas>().gameObject;
        GameObject hpBar = GameObject.Instantiate(_enemyHpBar.gameObject, transform.position, Quaternion.identity);
        hpBar.GetComponent<EnemyHpBar>().CurEntity = _entity;
        hpBar.transform.SetParent(enemyHpCanvas.transform, true);
    }



    protected virtual void FixedUpdate()
    {
        SwapAnimation();
        ChangeIdleBecausePlayer();
        ChangeIdleNearestEnemy();
        if (!_entity.IsAlive())
        {
            Dead();
            return;
        }
        _agent.speed = _entity.Speed;
        Movement();
    }


    protected virtual void Dead()
    {

        if (_dropItem != null && _countDrop > 0)
            BulletDrop.Drop(_dropItem, _countDrop, _forceDrop, transform.position);
        _animator.Play("Dead");
        _isDead = true;
        Component[] components = GetComponents<Component>();
        foreach (Component component in components)
        {
            // Пропускаем Transform и SpriteRenderer
            if (component is Transform || component is SpriteRenderer || component is Animator)
                continue;
            Destroy(component);
        }
        Destroy(gameObject, _deadTime);
    }

    protected virtual void Movement()
    {
        Agent.SetDestination(transform.position);
        if (IsIdle || IsFreeze)
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

    protected virtual void SwapAnimation()
    {
        if (IsFreeze)
        {
            Animator.speed = 0;
        }
        else
        {
            Animator.speed = 1;
        }
        if (IsIdle)
        {
            _animator.Play("Idle");
        }
        else
        {
            _animator.Play("Walk");
        }


    }
}
