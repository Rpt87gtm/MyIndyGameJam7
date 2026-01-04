using UnityEngine;

namespace bullets
{

    public class Bullet : MonoBehaviour
    {
        protected Vector2 _direction;

        public virtual void Shoot(Vector2 direction)
        {
            Debug.Log("shoot");
        }
    }

    public enum BulletType
    {
        Normal,
        Fire,
        Water,
        Cold,
        Electric
    }
}
