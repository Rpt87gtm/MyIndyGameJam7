using System.Collections.Generic;
using bullets;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemy : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Entity _entity;
    private Player _player;

    [SerializeField ]private List<BulletType> _bullets;
    [SerializeField] private float _rangeHold;
    [SerializeField] private float _shootTime = 0;
    private Shooter _shooter;
    private float _curShootTime = 0;

    public List<BulletType> Bullets => _bullets;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _entity = GetComponent<Entity>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _player = FindAnyObjectByType<Player>();
        _shooter = GetComponent<Shooter>();
        _curShootTime = _shootTime;
    }
    private void FixedUpdate()
    {
        _curShootTime -= Time.deltaTime;

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
        _agent.SetDestination(1 * _rangeHold * (transform.position - _player.transform.position).normalized);

        if (_curShootTime <= 0)
        {
            _shooter.TryShoot(_player.transform.position, _bullets);
            _curShootTime = _shootTime;
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
