using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{

    private Animator _animator;

    private const string pName = "State";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimation(PlayerController.PlayerAnimatorState state)
    {
        _animator.SetInteger(pName, (int)state);
    }
}
