using System.Collections.Generic;
using System.Linq;
using bullets;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemy : Enemy
{


    [SerializeField ]private List<BulletType> _bullets;
    [SerializeField] private float _rangeHold;
    [SerializeField] private float _shootTime = 0;
    private Shooter _shooter;
    private float _curShootTime = 0;

    public List<BulletType> Bullets => _bullets;

    protected override void Start()
    {
        base.Start();
        _shooter = GetComponent<Shooter>();
        _curShootTime = _shootTime;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Shoot();
    }

    protected override void Movement()
    {
        if (IsIdle)
            return;
        Agent.SetDestination(1 * _rangeHold * (transform.position - CurPlayer.transform.position).normalized);
    }

    protected void Shoot()
    {
        if (IsIdle)
            return;
        _curShootTime -= Time.deltaTime;
        if (_curShootTime <= 0)
        {
            _shooter.TryShoot(CurPlayer.transform.position, _bullets);
            _curShootTime = _shootTime;
        }
    }



}
