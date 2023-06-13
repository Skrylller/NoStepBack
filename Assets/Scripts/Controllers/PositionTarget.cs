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

    private Vector3 _targetPos;

    private void Start()
    {
        transform.position = _targetObj.position + _deviationPosition;
    }

    private void Update()
    {

        if (!_isLateUpdate)
        {
            MoveObject();
        }
    }

    private void LateUpdate()
    {
        if (_isLateUpdate)
        {
            MoveObject();
        }
    }

    public void SetTarget(Transform transform)
    {
        _targetObj = transform;
        CalculateTarget();
    }

    public void Teleport(Vector3 plusPos)
    {
        transform.position = CalculateTarget() + plusPos;
    }

    public void Teleport()
    {
        Teleport(Vector3.zero);
    }

    private Vector3 CalculateTarget()
    {
        _targetPos = new Vector3
            (
            _defaultPosX._isActive ? _defaultPosX._immutableValue : _targetObj.position.x,
            _defaultPosY._isActive ? _defaultPosY._immutableValue : _targetObj.position.y,
            _defaultPosZ._isActive ? _defaultPosZ._immutableValue : _targetObj.position.z
            ) + _deviationPosition;
        return _targetPos;
    }

    private void MoveObject()
    {
        CalculateTarget();
        transform.position = Vector3.Lerp(transform.position, _targetPos, _speed * Time.deltaTime);
    }
}
