using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour, ICapturedObject
{
    public enum RotationState
    {
        right,
        left
    }

    [SerializeField] private EnemyModel _model;

    [SerializeField] private EnemyPlayerObserver _enemyPlayerObserver;
    [SerializeField] private MovingController _movingController;
    [SerializeField] private StairsController _stairsController;
    [SerializeField] private LifeController _lifeController;
    [SerializeField] private EnemyAttackController _enemyAttackController;
    [SerializeField] private RandomTargetFinder _randomTargetFinder;
    [SerializeField] private JumpingController _jumpingController;
 
    [SerializeField] private Animator _footAnimator;
    [SerializeField] private ModeSwitcher _rotateSwitcher;

    [Header("Particles")]
    [SerializeField] private PullableObj _particleDeath;

    private Rigidbody2D _rbEnemy;
    private Transform _captureTarget;
    private bool _unactiveEnemy;

    [HideInInspector] public bool isNewTargetDelay;
    public bool isTeleportateDelay;
    public Action OnTeleportateToPlayer;

    public EnemyModel Model => _model;
    public bool IsDeath => _lifeController.HealthGet <= 0;

    private void Awake()
    {
        _rbEnemy = GetComponent<Rigidbody2D>();
        _lifeController.Init(_model);
    }

    private void Start()
    {
        _model.isRun = false;
        _enemyPlayerObserver.Init(_model);
        _movingController.Init(_model, _rbEnemy);
        _stairsController.Init(_model, _rbEnemy);
        if(_jumpingController != null)
            _jumpingController.Init(_model, _rbEnemy);
    }

    private void OnEnable()
    {
        _lifeController.OnDammage += Dammage;
        _lifeController.OnDeath += Death;
        _enemyPlayerObserver.OnView += ViewPlayer;
        _randomTargetFinder.FindNewTarget();
        StartCoroutine(NewTargetTimer());
        _enemyAttackController.StopAttack();
        EndCapture();
    }

    private void OnDisable()
    {
        _lifeController.OnDammage -= Dammage;
        _lifeController.OnDeath -= Death;
        _enemyPlayerObserver.OnView -= ViewPlayer;
    }

    private void Update()
    {
        CheckState();
        MoveToTarget();
        CheckTeleportate();
    }

    public void Restart()
    {
        _lifeController.Restart();
    }

    public void Capture(Transform position)
    {
        _unactiveEnemy = true;
        _enemyAttackController.StopAttack();
        _captureTarget = position;
    }

    public void EndCapture()
    {
        _unactiveEnemy = false;
    }

    public void Jump()
    {
        if (_jumpingController == null)
            return;

        if (_jumpingController.IsGrounded > 0)
            _jumpingController.Jump();
    }

    private void CheckState()
    {
        if(_rbEnemy.velocity.y > 0)
            _footAnimator.SetInteger("State", 3);

        else if (_rbEnemy.velocity.y < 0)
            _footAnimator.SetInteger("State", 4);

        else if (_rbEnemy.velocity.x != 0)
        {
            if (_model.isRun)
                _footAnimator.SetInteger("State", 2);
            else
                _footAnimator.SetInteger("State", 1);
        }
        else
            _footAnimator.SetInteger("State", 0);
    }

    private void MoveToTarget()
    {
        if (_unactiveEnemy)
            return;

        if (_randomTargetFinder.target == null)
            return;

        float directional;

        directional = (_randomTargetFinder.target.x - transform.position.x) / Mathf.Abs(_randomTargetFinder.target.x - transform.position.x);

        if (Mathf.Abs(_randomTargetFinder.target.x - transform.position.x) > _model.StopDistance)
        {
            if(_jumpingController.IsGrounded > 0)
                _movingController.MoveHorizontal(directional);
            _stairsController.CheckStair(directional);
        }
        else if (!isNewTargetDelay)
        {
            if (!_enemyPlayerObserver.isView)
            {
                NewTarget();
            }
            else if (Mathf.Abs(_randomTargetFinder.target.y - transform.position.y) > _model.VerticalStopDistance)
            {
                if (NewTarget())
                {
                    StartCoroutine(_enemyPlayerObserver.StopFind(2));
                    if (_model.IsCanTeleportate && _enemyPlayerObserver.isViewOnce && !_model.IsTeleportateAlways)
                        Invoke(nameof(TP), 2f);

                    void TP()
                    {
                        if (!isTeleportateDelay)
                            StartCoroutine(TeleportateTimer());
                    }
                }
            }

        }

        if (_jumpingController.IsGrounded > 0)
        {
            if (directional > 0)
                _rotateSwitcher.State = (int)RotationState.right;
            if (directional < 0)
                _rotateSwitcher.State = (int)RotationState.left;
        }

        bool NewTarget()
        {
            if (_enemyAttackController.isAttack)
                return false;

            _randomTargetFinder.FindNewTarget();
            StartCoroutine(NewTargetTimer());

            _model.isRun = false;

            return true;
        }
    }

    private void CheckTeleportate()
    {
        if (!_enemyPlayerObserver.isViewOnce)
            return;

        if (Math.Abs(Bootastrap.main.Player.transform.position.x - transform.position.x) > _model.TeleportateMaxDistance)
        {
            if (!isTeleportateDelay)
            {
                StartCoroutine(TeleportateTimer());
            }
        }
    }

    private void ViewPlayer(GameObject player)
    {
        _randomTargetFinder.target = player.transform.position;

        if(_model.CanRun)
            _model.isRun = true;
    }

    private void Dammage()
    {

    }

    private void Death()
    {
        PullsController.main.GetPull(_particleDeath).AddObj().SetTransform(transform.position);
        gameObject.SetActive(false);
    }

    private void CheckCapture()
    {
        if (!_lifeController.IsCapture)
            return;

        _rbEnemy.MovePosition(_captureTarget.position);
    }

    private IEnumerator NewTargetTimer()
    {
        isNewTargetDelay = true;
        yield return new WaitForSeconds(_model.NewTargetTimer);
        isNewTargetDelay = false;
    }
    private IEnumerator TeleportateTimer()
    {
        isTeleportateDelay = true;
        yield return new WaitForSeconds(UnityEngine.Random.Range(_model.TeleportationShowDelay.x, _model.TeleportationShowDelay.y));
        isTeleportateDelay = false;

        if (Math.Abs(Bootastrap.main.Player.transform.position.x - transform.position.x) > _model.TeleportateMaxDistance && _model.IsTeleportateAlways || !_enemyPlayerObserver.isView && !_model.IsTeleportateAlways)
        {
            _enemyAttackController.StopAttack();
            OnTeleportateToPlayer?.Invoke();
        }
    }
}
