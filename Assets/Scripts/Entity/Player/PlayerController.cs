using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IMousePositionVisitor
{
    private enum SpriteState
    {
        Right,
        Left
    }
    public enum PlayerAnimatorState
    {
        Idle,
        Walk,
        Jump,
        Fall
    }

    [SerializeField] private PlayerModel _model;

    [SerializeField] private MovingController _movingController;
    [SerializeField] private JumpingController _jumpingController;
    [SerializeField] private ShootingController _shootingController;
    [SerializeField] private PlayerAnimatorController _animatorController;
    [SerializeField] private PlayerStairsController _stairsController;

    [SerializeField] private ToTargetRotator2D _rotator;
    [SerializeField] private ModeSwitcher _spriteSwitcher;
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rbPlayer;

    private PlayerAnimatorState _state;
    public PlayerAnimatorState State
    {
        get { return _state; }
        set
        {
            _state = value;
            _animatorController.SetAnimation(value);
        }
    }

    private void Awake()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();

        _movingController.Init(_model, _rbPlayer);
        _jumpingController.Init(_model, _rbPlayer);
        _stairsController.Init(_model, _rbPlayer);
    }

    private void Update()
    {
        SetState();
    }

    public void MoveHorizontal(float directional)
    {
        if (_jumpingController.IsGrounded > 0)
        {
            _spriteSwitcher.State = directional < 0 ? (int)SpriteState.Left : (int)SpriteState.Right;
            _movingController.MoveHorizontal(directional);
            _stairsController.CheckStair(directional);
        }
    }

    public void MoveHorizontalStop()
    {
        if (_jumpingController.IsGrounded > 0)
            _movingController.MoveHorizontal(0);
    }

    public void Jump()
    {
        if (_jumpingController.IsGrounded > 0)
            _jumpingController.Jump();
    }

    public void CheckMouse(Vector2 mousePos)
    {
        float xBias = mousePos.x - _rotator.transform.position.x;

        if (_spriteSwitcher.State == (int)SpriteState.Right && xBias < 0)
            mousePos -= new Vector2(xBias * 2, 0);

        if(_spriteSwitcher.State == (int)SpriteState.Left)
        {
            float yBias = mousePos.y - _rotator.transform.position.y;

            mousePos -= new Vector2(0, yBias * 2);
            if (xBias < 0)
                mousePos -= new Vector2(xBias * 2, 0);
        }

        _rotator.Rotate(mousePos);
    }

    public void Shoot()
    {
        if(State == PlayerAnimatorState.Idle || 
            State == PlayerAnimatorState.Walk)
            _shootingController.Shoot();
    }

    private void SetState()
    {
        if (_rbPlayer.velocity.y > 0)
            State = PlayerAnimatorState.Jump;
        else if (_rbPlayer.velocity.y < 0)
            State = PlayerAnimatorState.Fall;
        else if (_rbPlayer.velocity.x != 0)
            State = PlayerAnimatorState.Walk;
        else
            State = PlayerAnimatorState.Idle;
    }
}
