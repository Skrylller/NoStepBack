using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAttackController : MonoBehaviour
{
    public enum HandState
    {
        idle,
        attack
    }

    [SerializeField] private Animator _bodyAnimator;
    [SerializeField] private Animator _handAnimator;
    [SerializeField] private float _attackDelay;
    [SerializeField] private BulletModel _missleModel;
    [SerializeField] private EnemyPlayerObserver _enemyPlayerObserver;

    private GameObject _player;
    private bool _isReadyAttack;

    [HideInInspector] public bool isAttack;

    private const string _playerLayout = "Player";

    private void OnEnable()
    {
        _isReadyAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_player == null && collision.gameObject.layer == LayerMask.NameToLayer(_playerLayout))
        {
            _player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            _player = null;
        }
    }

    private void Update()
    {
        CheckAttack();
    }

    private void CheckAttack()
    {
        if (_player == null || !_isReadyAttack || !_enemyPlayerObserver.isView)
            return;

        isAttack = true;

        _bodyAnimator.SetInteger("State", (int)HandState.attack);
        _handAnimator.SetInteger("State", (int)HandState.attack);
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        _isReadyAttack = false;
        yield return new WaitForSeconds(_attackDelay);
        _isReadyAttack = true;
    }

    public void StopAttack()
    {
        isAttack = false;

        _bodyAnimator.SetInteger("State", (int)HandState.idle);
        _handAnimator.SetInteger("State", (int)HandState.idle);
    }

    public void Shoot()
    {
        if (_player == null)
            return;

        BulletEntity missle = PullsController.main.GetPull(_missleModel.BulletPref).AddObj() as BulletEntity;

        float angle;

        if (_player.transform.position.x > transform.position.x)
            angle = 0;
        else
            angle = 180; 

        missle.Init(_missleModel, transform, angle);
    }
}
