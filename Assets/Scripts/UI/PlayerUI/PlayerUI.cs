using System;
using System.Collections.Generic;
using bullets;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public MoveUpDown moveUpDown;
    private Action<List<BulletType>> _callback;
    void Start()
    {
        moveUpDown.MoveDown(true);
    }

    public void StartReload(Action<List<BulletType>> callback)
    {
        _callback = callback;
        moveUpDown.MoveUp();
    }

    public void StopReload()
    {
        moveUpDown.MoveDown();
        List<BulletType> bullets = new() { BulletType.Electric, BulletType.Normal };
        if (_callback != null)
        {
            _callback(bullets);
            _callback = null;
        }
    }
}
