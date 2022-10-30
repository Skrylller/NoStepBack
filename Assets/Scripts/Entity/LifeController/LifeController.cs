using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour, ISliderVisitor
{

    [SerializeField] private PullableObj _particleDammage;
    private PullObjects _partPull;

    private ILifeModel _model;

    private uint _health;
    public uint HealthGet { get { return _health; } }
    private uint Health 
    { 
        set 
        {
            _health = value;
            sliderValue = (float)_health / _model.MaxHealth;
        } 
    }

    public Action OnDammage;
    public Action OnHill;
    public Action OnDeath;

    public Action<float> OnChange { get; set; }
    public float sliderValue 
    {
        set 
        {
            OnChange?.Invoke(value);
        } 
    }

    private void Start()
    {
        _partPull = PullsController.main.GetPull(_particleDammage);
    }

    public void Init(ILifeModel model)
    {
        _model = model;
        Health = _model.MaxHealth;
    }

    public void TakeDammage(uint dammage, Vector2 point, float angle)
    {
        PullableObj part = _partPull.AddObj();
        part.SetTransform(point, angle);

        if (_health <= dammage)
        {
            Health = 0;
            OnDeath?.Invoke();
        }
        else
        {
            Health = HealthGet - dammage;
            OnDammage?.Invoke();
        }
    }

    public void Hill(uint hill)
    {
        if (_model.MaxHealth <= hill + _health)
        {
            Health = _model.MaxHealth;
        }
        else
        {
            Health = HealthGet + hill;
        }

        OnHill?.Invoke();
    }
}

public interface ILifeModel
{
    uint MaxHealth { get; }  
}
