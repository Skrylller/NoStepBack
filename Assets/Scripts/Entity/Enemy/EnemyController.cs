using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
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

    [SerializeField] private Animator _footAnimator;
    [SerializeField] private ModeSwitcher _rotateSwitcher;

    private Rigidbody2D _rbEnemy;

    private Vector2 _target;

    private void Awake()
    {
        _rbEnemy = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _enemyPlayerObserver.Init(_model);
        _movingController.Init(_model, _rbEnemy);
        _stairsController.Init(_model, _rbEnemy);
    }

    private void OnEnable()
    {
        _enemyPlayerObserver.OnView += ViewPlayer;
    }

    private void OnDisable()
    {
        _enemyPlayerObserver.OnView -= ViewPlayer;
    }

    private void Update()
    {
        CheckState();
        if (_target != null)
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
        float directional;

        directional = (_target.x - transform.position.x) / Mathf.Abs(_target.x - transform.position.x);

        if (Vector2.Distance(_target, transform.position) > _model.StopDistance)
        {
            _movingController.MoveHorizontal(directional);
            _stairsController.CheckStair(directional);
        }

        if (directional > 0)
            _rotateSwitcher.State = (int)RotationState.right;
        if (directional < 0)
            _rotateSwitcher.State = (int)RotationState.left;
    }

    private void ViewPlayer(GameObject player)
    {
        _target = player.transform.position;

    }
}
