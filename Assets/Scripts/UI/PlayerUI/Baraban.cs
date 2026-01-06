using System.Collections.Generic;
using System.Linq;
using bullets;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Baraban : MonoBehaviour
{
    public List<BarabanBullet> bullets;
    public List<BulletSlot> slots;
    private Queue<BulletSlot> slotsQueue = new();
    private Queue<BulletSlot> EmptySlotsQueue = new();
    public void Awake()
    {
        foreach (var slot in slots)
        {
            EmptySlotsQueue.Enqueue(slot);
        }
    }

    public void AddBullet(BulletType type)
    {
        Debug.Log(EmptySlotsQueue.Count);
        var slot = EmptySlotsQueue.Dequeue();
        var barabanBullet = bullets.Find(bb => bb.Type == type);
        slot.SetSlot(barabanBullet);
        slotsQueue.Enqueue(slot);
    }
    public void RemoveBullet()
    {
        var slot = slotsQueue.Dequeue();
        slot.ClearSlot();
        EmptySlotsQueue.Enqueue(slot);
    }
}
[System.Serializable]
public struct BarabanBullet
{
    public BulletType Type;
    public Sprite Image;
}