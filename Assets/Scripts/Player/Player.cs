using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float speed = 4;

    private InputSystem_Actions inputActions;
    private Vector2 input = new();
    private Rigidbody2D rb;

    private void Awake()
    {
        inputActions = new();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void FixedUpdate()
    {
        input = inputActions.Player.Move.ReadValue<Vector2>();
        Debug.Log(input);
        Vector2 newPosition = rb.position + input * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }


}
