using bullets;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class BulletItem : MonoBehaviour
{
    [SerializeField] private BulletType _type;
    public BulletType Type => _type;
    public void Dest()
    {
        Destroy(gameObject);
    }

}
