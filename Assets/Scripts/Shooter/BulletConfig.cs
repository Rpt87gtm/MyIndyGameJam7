using bullets;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullets/Bullet")]
public class BulletConfig : ScriptableObject
{
    public BulletType bulletType;
    public GameObject prefab;
}
