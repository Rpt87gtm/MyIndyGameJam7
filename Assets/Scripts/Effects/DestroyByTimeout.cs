using UnityEngine;

public class DestroyByTimeout : MonoBehaviour
{
    public float lifeTime = 1f;
    private float currentTime = 0;

    void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        if (currentTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
