using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTarget : MonoBehaviour
{
    [System.Serializable]
    private class ImmutablePositionValue
    {
        public bool _isActive;
        public float _immutableValue;
    }

    [SerializeField] private Transform _targetObj;

    [SerializeField] private bool _isLateUpdate;

    [SerializeField] private float _speed;

    [Space]
    [SerializeField] private Vector3 _deviationPosition;
    [SerializeField] private ImmutablePositionValue _defaultPosX;
    [SerializeField] private ImmutablePositionValue _defaultPosY;
    [SerializeField] private ImmutablePositionValue _defaultPosZ;

    private Vector3 _movePosition;

    private void Start()
    {
        transform.position = _targetObj.position + _deviationPosition;
        _movePosition = transform.position;
    }

    private void Update()
    {
        CalculateTarget();

        if (!_isLateUpdate)
            MoveObject();
    }

    private void LateUpdate()
    {
        if (_isLateUpdate)
            MoveObject();
    }

    private void CalculateTarget()
    {
        Vector3 targetPosition = new Vector3
            (
            _defaultPosX._isActive ? _defaultPosX._immutableValue : _targetObj.position.x,
            _defaultPosY._isActive ? _defaultPosY._immutableValue : _targetObj.position.y,
            _defaultPosZ._isActive ? _defaultPosZ._immutableValue : _targetObj.position.z
            ) + _deviationPosition;

        _movePosition = Vector3.LerpUnclamped(_movePosition, targetPosition, _speed * Time.deltaTime);
    }

    private void MoveObject()
    {
        transform.position = _movePosition;
    }
}
