using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Characters/Player")]
public class PlayerModel : ScriptableObject, MovingModel, JumpingModel, ClimpingModel
{
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] float _stairClimpingSpeed;

    public float Speed { get { return _speed; } }
    public float JumpForce { get { return _jumpForce; } }
    public float StairClimpingSpeed { get { return _stairClimpingSpeed; } }
}
