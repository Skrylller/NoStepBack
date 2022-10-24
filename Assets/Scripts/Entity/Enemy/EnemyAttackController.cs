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

    [SerializeField] private Animator _handAnimator;
    [SerializeField] private float _attackDelay;

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
        if (_player == null || !_isReadyAttack)
            return;

        isAttack = true;
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
        _handAnimator.SetInteger("State", (int)HandState.idle);
    }
}
