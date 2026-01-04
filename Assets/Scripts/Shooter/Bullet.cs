using System.Collections;
using UnityEngine;

namespace bullets
{
    public class Bullet : MonoBehaviour
    {
        public float lifeTime = 7;
        protected Vector2 _direction;

        public virtual void Shoot(Vector2 direction)
        {
            Debug.Log("shoot");
            StartCoroutine(DestroyByTimeout());
        }

        private IEnumerator DestroyByTimeout()
        {
            yield return new WaitForSeconds(lifeTime);
            DestroyBullet();
        }

        public virtual void DestroyBullet()
        {
            Debug.Log("Destroy bullet");
            Destroy(gameObject);
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
