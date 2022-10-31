using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureArea : MonoBehaviour
{
    [SerializeField] private int _tapCount;
    [SerializeField] private int _layer;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private EnemyAttackController attackController;
    private GameObject obj;

    public int TapCount => _tapCount;
    public Transform TargetPosition => _targetPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == _layer)
        {
            obj = collision.gameObject;
        }
    }

    public void CheckPlayer()
    {
        if(obj == null)
        {
            attackController.StopAttack();
        }
    }

    public void EndCapture()
    {
        attackController.StopAttack();
    }

    public void OnDisable()
    {
        obj = null;
    }
}
