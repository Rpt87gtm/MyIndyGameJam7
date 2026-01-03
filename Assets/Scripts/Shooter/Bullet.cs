using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Vector2 _target;

    public void SetTarget(Vector2 target)
    {
        _target = target;
    }

    public virtual void Shoot()
    {
        Debug.Log("shoot");
    }
}
