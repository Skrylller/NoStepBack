using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private Animator _footAnimator;
    [SerializeField] private ModeSwitcher _rotateSwitcher;
    
    [Header("Particles")]
    [SerializeField] private PullableObj _particleDeath;

    private Rigidbody2D _rbEnemy;
    private Transform _captureTarget;
    private bool _unactiveEnemy;

    [HideInInspector] public bool isNewTargetDelay;


    private void Awake()
    {
        _rbEnemy = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _model.isRun = false;
        _lifeController.Init(_model);
        _enemyPlayerObserver.Init(_model);
        _movingController.Init(_model, _rbEnemy);
        _stairsController.Init(_model, _rbEnemy);
    }

    private void OnEnable()
    {
        _lifeController.OnDammage += Dammage;
        _lifeController.OnDeath += Death;
        _enemyPlayerObserver.OnView += ViewPlayer;
        isNewTargetDelay = false;
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
    }

    private void CheckState()
    {
        if (_rbEnemy.velocity.x != 0)
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
                    StartCoroutine(_enemyPlayerObserver.StopFind(2));
            }

        }

        if (directional > 0)
            _rotateSwitcher.State = (int)RotationState.right;
        if (directional < 0)
            _rotateSwitcher.State = (int)RotationState.left;

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

    private void ViewPlayer(GameObject player)
    {
        _randomTargetFinder.target = player.transform.position;

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
}
