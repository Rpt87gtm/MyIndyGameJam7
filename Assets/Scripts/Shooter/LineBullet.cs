using bullets;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LineBullet : Bullet
{

    [SerializeField ]private Effect _effect;
    [SerializeField] private int _baseDmg;
    public float speed = 10;
    private Rigidbody2D rb;

    public GameObject destroyParticle;

    public override void Shoot(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        var movement = direction * speed;
        base.Shoot(direction);
        rb.linearVelocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO здесь получай компонент на который попадание аффектит

        if (collision.gameObject.TryGetComponent<Entity>(out Entity entity))
        {
            entity.ChangeHp(_baseDmg * -1);
            if (_effect  != null)
                entity.AddEffect(_effect);
        }


        if (collision.contactCount > 0)
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 collisionNormal = contact.normal;
            Vector2 collisionPoint = contact.point;

            // Рассчитываем угол для параллельности поверхности
            float angle = Mathf.Atan2(collisionNormal.y, collisionNormal.x) * Mathf.Rad2Deg - 90f;
            Quaternion particleRotation = Quaternion.Euler(0, 0, angle);

            Instantiate(destroyParticle, collisionPoint, particleRotation);
        }
        DestroyBullet();
    }

    public override void DestroyBullet()
    {
        base.DestroyBullet();
    }
}
