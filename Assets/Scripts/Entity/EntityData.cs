using UnityEngine;
using System;



[Serializable]
public class EntityData
{
    [Min(0)]
    [SerializeField] private int _maxHp;
    [Min(0)]
    [SerializeField] private float _defaultSpeed;
    [Min(0)]
    [SerializeField] private int _curHp;
    [Min(0)]
    [SerializeField] private float _curSpeed;
    public int DefaultHp => _maxHp;
    public float DefaultSpeed => _defaultSpeed;

    void Start()
    {
        _curHp = _maxHp;
    }


    public void SetDefaultSpeed()
    {
        _curSpeed = _defaultSpeed;
    }

    public void HpSetDefault()
    {
        _curHp = _maxHp;
    }

    public void ChangeSpeed(float speed)
    {
        _curSpeed += speed;
        if (_curSpeed < 0)
            _curSpeed = 0;
    }

    public void ChangeHp(int hp)
    {
        _curHp = hp;
        if (_curHp > _maxHp)
            _curHp = _maxHp;
        if (_curHp < 0)
            _curHp = 0;
    }


}
