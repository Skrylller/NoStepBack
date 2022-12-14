using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AnimationEventController : MonoBehaviour
{
    [SerializeField] private UnityEvent _action;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Action()
    {
        _action?.Invoke();
    }

    public void SetAnimatorState(int value)
    {
        _animator.SetInteger("State", value);
    }
}
