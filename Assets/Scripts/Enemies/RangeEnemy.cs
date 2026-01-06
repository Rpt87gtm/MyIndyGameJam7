using bullets;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{


    [SerializeField] private List<BulletType> _bullets;
    [SerializeField] private float _rangeHold;
    [SerializeField] private float _shootTime = 0;
    [SerializeField] private float _wallTime = 0;

    [SerializeField] private float shootCooldown = 0.2f;
    private Shooter _shooter;
    private float _curShootTime = 0;
    private float _curWallTime = 0;
    private bool _isShoot;
    public float fleeDistance = 5f; // ���������, �� ������� ����� �������
    public float safeDistance = 10f; // ����������� ��������� ������������
    public float approachDistance = 15f; // Дистанция, на которой начинаем приближаться

    public List<BulletType> Bullets => _bullets;

    protected override void Start()
    {
        base.Start();
        _shooter = GetComponent<Shooter>();
        _curShootTime = _shootTime;
        _curWallTime = _wallTime;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Movement()
    {
        Agent.SetDestination(transform.position);
        if (IsIdle || IsFreeze)
            return;
        if (_isShoot)
        {
            Shoot();
            return;
        }



        if (_curWallTime <= 0)
        {
            _isShoot = true;
        }

        _curWallTime -= Time.deltaTime;


        FleeFromPlayer();
    }

    protected void Shoot()
    {
        if (IsIdle || IsFreeze)
            return;
        _curShootTime -= Time.deltaTime;
        if (_curShootTime <= 0)
        {
            _shooter.TryShoot(CurPlayer.transform.position, _bullets, shootCooldown);
            _curShootTime = _shootTime;
            _isShoot = false;
            _curWallTime = _wallTime;
        }
    }

    protected override void SwapAnimation()
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
            Animator.Play("Idle");
        }
        else if (_isShoot)
        {
            Animator.Play("Shoot");
        }
        else
        {
            Animator.Play("Walk");
        }
    }

    void FleeFromPlayer()
    {
        Vector3 toPlayer = CurPlayer.transform.position - transform.position;
        float distanceToPlayer = toPlayer.magnitude;

        // Если дистанция маленькая - убегаем от игрока
        if (distanceToPlayer < safeDistance)
        {
            // Вычисляем направление побега (противоположное игроку)
            Vector3 fleeDirection = -toPlayer.normalized;

            // Рассчитываем точку побега
            Vector3 fleeTarget = transform.position + fleeDirection * fleeDistance;

            // Устанавливаем точку назначения
            Agent.SetDestination(fleeTarget);
        }
        // Если дистанция большая - приближаемся к игроку
        else if (distanceToPlayer > approachDistance)
        {
            // Двигаемся прямо к игроку
            Agent.SetDestination(CurPlayer.transform.position);
        }
        // Если дистанция средняя - остаемся на месте или прекращаем движение
        else
        {
            Agent.ResetPath(); // Останавливаем движение
                               // Или можно установить Agent.isStopped = true;
        }
    }

}
