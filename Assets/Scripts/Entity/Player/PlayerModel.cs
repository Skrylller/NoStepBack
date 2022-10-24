using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Characters/Player")]
public class PlayerModel : ScriptableObject, MovingModel, JumpingModel, ClimpingModel, SitModel, LifeModel
{
    [Header("Move")]
    [SerializeField] uint _maxHealth;
    [Header("Move")]
    [SerializeField] float _speed;
    [SerializeField] bool _canRun;
    [SerializeField] float _runFactor;
    [Header("Jump")]
    [SerializeField] float _jumpForce;
    [Header("Sit")]
    [SerializeField] float _crawlingSpeedMultiplier;
    [Header("Climping")]
    [SerializeField] float _stairClimpingSpeed;
    [SerializeField] float _stairClimpingHeight;

    [HideInInspector] public bool isRun { get; set; }
    [HideInInspector] public bool isSit { get; set; }

    public uint MaxHealth { get { return _maxHealth; } }
    public float Speed 
    { 
        get 
        { 
            return isSit ? _speed * _crawlingSpeedMultiplier : isRun ? _speed * _runFactor : _speed; 
        }
    }
    public bool CanRun { get { return _canRun; } }
    public float RunFactor { get { return _runFactor; } }
    public float JumpForce { get { return _jumpForce; } }
    public float CrawlingSpeedMultiplier { get { return _crawlingSpeedMultiplier; } }
    public float StairClimpingSpeed { get { return _stairClimpingSpeed; } }
    public float StairClimpingHeight { get { return _stairClimpingHeight; } }
}
