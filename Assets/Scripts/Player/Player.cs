using System.Collections;
using System.Collections.Generic;
using bullets;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Shooter), typeof(Entity))]
public class Player : MonoBehaviour
{
    public float shootRotationCooldown = 1f;
    private Reload reload;

    private InputSystem_Actions inputActions;
    private Vector2 input = new();
    private Rigidbody2D rb;
    private bool _isFlipped = false;
    private Shooter shooter;
    private bool _isShootFliped = false;
    private Coroutine flipCooldownCoroutine;
    private Entity _entity;

    [SerializeField] private List<BulletType> _bullets;

    private void Awake()
    {
        inputActions = new();
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
        _entity = GetComponent<Entity>();
        reload = GetComponent<Reload>();
    }

    private void Start()
    {

    }

    public void SetBullets(List<BulletType> bullets)
    {
        _bullets.AddRange(bullets);
    }
    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Attack.performed += OnAttackPressed;
    }
    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttackPressed;
        inputActions.Disable();
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
            rb.MovePosition(transform.position);
            return;
        }
        if (reload.IsReloading)
        {
            input = Vector2.zero;
        }
        else
        {
            input = inputActions.Player.Move.ReadValue<Vector2>();
        }
        var newPosition = rb.position + input * _entity.Speed * Time.fixedDeltaTime;
        if (!_isShootFliped) { TryFlip(input); }
        rb.MovePosition(newPosition);
    }

    private void OnAttackPressed(InputAction.CallbackContext context)
    {
        if (_entity.IsFreeze)
            return;
        if (reload.IsReloading)
        {
            return;
        }
        var pos = GetMouseWorldPosition();
        Vector2 pos2d = new(pos.x, pos.y);
        FlipToPos(pos2d);
        if (_bullets.Count <= 0)
        {
            Debug.Log("I need more bullets");
            return;
        }
        var bullets = new List<BulletType>(_bullets);
        if (shooter.TryShoot(pos2d, bullets))
        {
            Debug.Log("clear bullets");
            _bullets.Clear();
            if (flipCooldownCoroutine != null)
            {
                StopCoroutine(flipCooldownCoroutine);
            }
            flipCooldownCoroutine = StartCoroutine(FlipCooldown(shootRotationCooldown * bullets.Count));
        }
    }

    private IEnumerator FlipCooldown(float cooldown)
    {
        _isShootFliped = true;
        yield return new WaitForSeconds(cooldown);
        _isShootFliped = false;
    }
    Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)
        );
    }

    private void FlipToPos(Vector2 pos)
    {
        if (pos.x > transform.position.x && _isFlipped || pos.x < transform.position.x && !_isFlipped)
        {
            Flip();
        }
    }

    private bool TryFlip(Vector2 direction)
    {
        if (direction.x > 0 && _isFlipped || direction.x < 0 && !_isFlipped)
        {
            Flip();
            return true;
        }
        return false;
    }

    private void Flip()
    {
        _isFlipped = !_isFlipped;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Dead()
    {
        Destroy(gameObject);
        Debug.Log("�� ������");
    }
}
