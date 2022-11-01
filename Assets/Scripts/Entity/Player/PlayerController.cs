using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IMousePositionVisitor, ICapturedObject
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
        Fall,
        Sit,
        Crawl,
        Climb,
        Capture,
    }

    [SerializeField] private PlayerModel _model;

    [SerializeField] private MovingController _movingController;
    [SerializeField] private JumpingController _jumpingController;
    [SerializeField] private ShootingController _shootingController;
    [SerializeField] private PlayerAnimatorController _animatorController;
    [SerializeField] private StairsController _stairsController;
    [SerializeField] private PlayerSitController _sitController;
    [SerializeField] private LifeController _lifeController;
    [SerializeField] private PlayerUI _playerUI;

    [SerializeField] private List<ToTargetRotator2D> _rotators;
    [SerializeField] private ModeSwitcher _spriteSwitcher;
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rbPlayer;
    private Transform _captureTarget;

    private bool stopInput;

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

    private const string layerPlayer = "Player";
    private const string layerPlatform = "Platform";

    private void Awake()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();

        _movingController.Init(_model, _rbPlayer);
        _jumpingController.Init(_model, _rbPlayer);
        _stairsController.Init(_model, _rbPlayer);
        _sitController.Init(_model, _rbPlayer);
        _lifeController.Init(_model);
    }

    private void OnEnable()
    {
        _stairsController.OnClimb += PlatformOff;
        _stairsController.OnStopClimb += PlatformOn;
    }

    private void OnDisable()
    {
        _stairsController.OnClimb -= PlatformOff;
        _stairsController.OnStopClimb -= PlatformOn;
    }

    private void Update()
    {
        SetState();
    }

    private void FixedUpdate()
    {
        CheckCapture();
    }

    public void MoveHorizontal(float directional)
    {
        if (stopInput)
            return;

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
        if (stopInput)
            return;

        if (_stairsController.Collider != null)
            return;

        if (_model.isSit)
        {
            PlatformOff();
            Invoke(nameof(PlatformOn), 1f);
            return;
        }

        if (_jumpingController.IsGrounded > 0)
            _jumpingController.Jump();
    }

    public void UpClimb()
    {
        if (stopInput)
            return;

        if (_stairsController.Collider != null)
        {
            _stairsController.ClimbStair(1);
        }
    }

    public void JumpUp()
    {
        if (stopInput)
            return;

        if (_stairsController.Collider != null)
        {
            _stairsController.ClimbStair(0);
            return;
        }
    }

    public void CheckMouse(Vector2 mousePos)
    {
        foreach(ToTargetRotator2D rotator in _rotators)
        {
            SetRotator(rotator, mousePos);
        }

        void SetRotator(ToTargetRotator2D rotator, Vector2 mousePos)
        {
            float xBias = mousePos.x - rotator.transform.position.x;

            if (_spriteSwitcher.State == (int)SpriteState.Right && xBias < 0)
                mousePos -= new Vector2(xBias * 2, 0);

            if (_spriteSwitcher.State == (int)SpriteState.Left)
            {
                float yBias = mousePos.y - rotator.transform.position.y;

                mousePos -= new Vector2(0, yBias * 2);
                if (xBias < 0)
                    mousePos -= new Vector2(xBias * 2, 0);
            }

            rotator.Rotate(mousePos);
        }
    }

    public void Shoot()
    {
        if (stopInput)
            return;

        if (State == PlayerAnimatorState.Idle || 
            State == PlayerAnimatorState.Walk)
            _shootingController.Shoot();
    }

    private void SetState()
    {
        if (_lifeController.IsCapture)
            State = PlayerAnimatorState.Capture;
        else if (_model.isSit && _rbPlayer.velocity.x == 0)
            State = PlayerAnimatorState.Sit;
        else if (_model.isSit && _rbPlayer.velocity.x != 0)
            State = PlayerAnimatorState.Crawl;
        else if (_rbPlayer.gravityScale == 0 && (_rbPlayer.velocity.y != 0 || _rbPlayer.velocity.x != 0))
            State = PlayerAnimatorState.Climb;
        else if (_rbPlayer.velocity.y > 0)
            State = PlayerAnimatorState.Jump;
        else if (_rbPlayer.velocity.y < 0)
            State = PlayerAnimatorState.Fall;
        else if (_rbPlayer.velocity.x != 0)
            State = PlayerAnimatorState.Walk;
        else
            State = PlayerAnimatorState.Idle;
    }

    public void Sit()
    {
        if (stopInput)
            return;

        if (_stairsController.Collider != null)
        {
            _sitController.Sit(false);
            _stairsController.ClimbStair(-1);
            return;
        }

        if (_jumpingController.IsGrounded > 0)
            _sitController.Sit(true);
    }

    public void SitUp()
    {
        if (_stairsController.Collider != null)
        {
            _stairsController.ClimbStair(0);
            return;
        }

        _sitController.Sit(false);
    }

    public void SetWeapon(int weapon)
    {
        _shootingController.SetWeapon((WeaponModel.WeaponType)weapon);
    }

    public void Capture(Transform position)
    {
        stopInput = true;
        _captureTarget = position;
        _playerUI.ActiveCapture();
        SitUp();
        MoveHorizontalStop();
    }

    public void EndCapture()
    {
        _playerUI.DeactiveCapture();
        stopInput = false;
    }

    private void CheckCapture()
    {
        if (!_lifeController.IsCapture)
            return;

        _rbPlayer.MovePosition(_captureTarget.position);
    }

    private void PlatformOn()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(layerPlayer), LayerMask.NameToLayer(layerPlatform), false);
    }
    private void PlatformOff()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(layerPlayer), LayerMask.NameToLayer(layerPlatform), true);
    }
}
