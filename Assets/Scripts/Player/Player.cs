using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Shooter))]
public class Player : MonoBehaviour
{
    public float speed = 4;

    private InputSystem_Actions inputActions;
    private Vector2 input = new();
    private Rigidbody2D rb;
    private Shooter shooter;

    private void Awake()
    {
        inputActions = new();
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {

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
        input = inputActions.Player.Move.ReadValue<Vector2>();
        Vector2 newPosition = rb.position + input * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    private void OnAttackPressed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        var pos = GetMouseWorldPosition();
        //Debug.Log(pos);
        shooter.Shoot(new(pos.x, pos.y));
    }
    Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)
        );
    }

}
