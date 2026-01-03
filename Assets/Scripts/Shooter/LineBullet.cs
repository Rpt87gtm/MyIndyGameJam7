using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LineBullet : Bullet
{
    public float speed = 10;
    protected Vector2 moveDirection = new();
    private Rigidbody2D rb;

    public override void Shoot()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        base.Shoot();
        moveDirection = (_target - position2D).normalized;
        var movement = moveDirection * speed;
        Debug.Log(movement);
        rb.linearVelocity = movement;
    }
}
