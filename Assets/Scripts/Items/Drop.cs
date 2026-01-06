using UnityEngine;

public class BulletDrop : MonoBehaviour
{

    static public void Drop(GameObject bullet, int count, float force, Vector2 spawnPosition)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject bulletInstance = GameObject.Instantiate(bullet);
            bulletInstance.transform.position = spawnPosition;

            // –асчет угла дл€ равномерного распределени€
            float angle = i * (360f / count);
            Vector2 direction = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }
}


