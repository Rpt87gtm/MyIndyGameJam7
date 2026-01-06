using System.Collections.Generic;
using bullets;
using UnityEngine;

public class EntitySupport : MonoBehaviour
{
    [SerializeField] private Entity target;
    [SerializeField] private Effect _fire;
    [SerializeField] private Effect _water;
    [SerializeField] private Effect _electric;
    [SerializeField] private Effect _poison;
    [SerializeField] private Effect _frozen;


    [ContextMenu(nameof(_fire))]
    public void Fire()
    {
        target.AddEffect(_fire);
    }
    [ContextMenu(nameof(_water))]

    public void Water()
    {
        target.AddEffect(_water);
    }


    [ContextMenu(nameof(_electric))]

    public void Electric()
    {
        target.AddEffect(_electric);
    }
    [ContextMenu(nameof(_poison))]

    public void Poison()
    {
        target.AddEffect(_poison);
    }

    [ContextMenu(nameof(_frozen))]
    public void Frozen()
    {
        target.AddEffect(_frozen);
    }
}
