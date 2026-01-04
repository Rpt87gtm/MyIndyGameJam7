using bullets;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LineBullet : Bullet
{
    public float speed = 10;
    private Rigidbody2D rb;

    public override void Shoot(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        var movement = direction * speed;
        base.Shoot(direction);
        Debug.Log(movement);
        rb.linearVelocity = movement;
    }
}
