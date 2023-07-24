using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWorld : MonoBehaviour
{
    public enum EnemyWorldState
    {
        idle,
        world,
    }

    [SerializeField] private Transform _startPoint;
    [SerializeField] private EnemyController _enemy;

    [SerializeField] private AnimationEventController _enemyStartAnim;

    [SerializeField] private bool _startActive;

    private void Start()
    {
        if (!_startActive)
            _enemy.gameObject.SetActive(false);
    }

    public void Appearance()
    {
        if (_enemy.gameObject.activeSelf || _enemyStartAnim.GetState() == (int)EnemyWorldState.world)
            return;

        _enemy.transform.position = _startPoint.position;
        _enemyStartAnim.transform.position = _startPoint.position;
        _enemyStartAnim.SetAnimatorState((int)EnemyWorldState.world);
    }

    public void Hide()
    {
        _enemyStartAnim.transform.position = _enemy.transform.position;

        _enemyStartAnim.SetAnimatorState((int)EnemyWorldState.idle);
    }

    public void EnemyActive()
    {
        _enemy.gameObject.SetActive(true);

        _enemy.Restart();
    }
}
