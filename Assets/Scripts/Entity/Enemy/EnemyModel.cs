using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Characters/Enemy")]
public class EnemyModel : ScriptableObject, IPlayerObsrver, IMovingModel, IClimpingModel, ILifeModel, IJumpingModel
{
    [Header("Life")]
    [SerializeField] uint _maxHealth;
    [Header("Move")]
    [SerializeField] private float _speed;
    [SerializeField] private float _slowDownSpeed;
    [SerializeField] bool _canRun;
    [SerializeField] private float _runFactor;
    [Header("Jump")]
    [SerializeField] private float _jumpForce;
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
    [SerializeField] private float _verticalStopDistance;
    [SerializeField] private float _newTargetDelay;
    [Header("Teleportation")]
    [SerializeField] private bool _isCanTeleportate;
    [SerializeField] private bool _isTeleportateAlways;
    [SerializeField] private int _teleportateMinDistance;
    [SerializeField] private int _teleportateMaxDistance;
    [SerializeField] private Vector2 _teleportateHideDelay;
    [SerializeField] private Vector2 _teleportateShowDelay;

    [HideInInspector] public bool isRun { get; set; }

    public uint MaxHealth { get { return _maxHealth; } }
    public float Speed 
    { 
        get 
        { 
            return _canRun ? isRun ? _speed * _runFactor : _speed : _speed; 
        }
    }
    public float SlowDownSpeed => _slowDownSpeed;
    public bool CanRun { get { return _canRun; } }
    public float RunFactor { get { return _runFactor; } }
    public float JumpForce => _jumpForce;
    public bool IsViewAlways { get { return _isViewAlways; } }
    public float ViewDistance { get { return _viewDistance; } }
    public float MinDistance { get { return _minDistance; } }
    public float StairClimpingSpeed { get { return _stairClimpingSpeed; } }
    public float StairClimpingHeight { get { return _stairClimpingHeight; } }
    public float StopDistance { get { return _stopDistance; } }
    public float VerticalStopDistance { get { return _verticalStopDistance; } }
    public float NewTargetTimer { get { return _newTargetDelay; } }
    public bool IsCanTeleportate { get { return _isCanTeleportate; } }
    public bool IsTeleportateAlways { get { return _isTeleportateAlways; } }
    public int TeleportateMinDistance { get { return _teleportateMinDistance; } }
    public int TeleportateMaxDistance { get { return _teleportateMaxDistance; } }
    public Vector2 TeleportationHideDelay => _teleportateHideDelay;
    public Vector2 TeleportationShowDelay => _teleportateShowDelay;
}

public interface IPlayerObsrver
{
    bool IsViewAlways { get; }
    float ViewDistance { get; }
    float MinDistance { get; }
}
