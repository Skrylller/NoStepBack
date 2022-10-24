using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    private LifeModel _model;

    private uint _health;
    public uint Health { get { return _health; } }

    public Action OnDammage;
    public Action OnHill;
    public Action OnDeath;

    public void Init(LifeModel model)
    {
        _model = model;
        _health = _model.MaxHealth;
    }

    public void TakeDammage(uint dammage)
    {
        if(_health <= dammage)
        {
            _health = 0;
            OnDeath?.Invoke();
        }
        else
        {
            _health -= dammage;
            OnDammage?.Invoke();
        }
    }

    public void Hill(uint hill)
    {
        if (_model.MaxHealth <= hill + _health)
        {
            _health = _model.MaxHealth;
        }
        else
        {
            _health += hill;
        }

        OnHill?.Invoke();
    }
}

public interface LifeModel
{
    uint MaxHealth { get; }  
}
