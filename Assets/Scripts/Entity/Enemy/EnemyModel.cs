using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Characters/Enemy")]
public class EnemyModel : ScriptableObject, IPlayerObsrver, MovingModel, ClimpingModel
{
    [Header("Move")]
    [SerializeField] private float _speed;
    [SerializeField] private float _runFactor;
    [Header("Observer")]
    [SerializeField] private bool _isViewAlways;
    [SerializeField] private float _viewDistance;
    [SerializeField] private float _minDistance;
    [Header("Climping")]
    [SerializeField]
    private float _stairClimpingSpeed = 0;
    [SerializeField] private float _stairClimpingHeight;
    [Header("Enemy")]
    [SerializeField] private float _stopDistance;

    [HideInInspector] public bool isRun { get; set; }

    public float Speed 
    { 
        get 
        { 
            return isRun ? _speed * _runFactor : _speed; 
        } 
    }
    public float RunFactor { get { return _runFactor; } }
    public bool IsViewAlways { get { return _isViewAlways; } }
    public float ViewDistance { get { return _viewDistance; } }
    public float MinDistance { get { return _minDistance; } }
    public float StairClimpingSpeed { get { return _stairClimpingSpeed; } }
    public float StairClimpingHeight { get { return _stairClimpingHeight; } }
    public float StopDistance { get { return _stopDistance; } }
}

public interface IPlayerObsrver
{
    bool IsViewAlways { get; }
    float ViewDistance { get; }
    float MinDistance { get; }
}
