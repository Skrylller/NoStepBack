using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionObject : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _areaAction;
    [SerializeField] private ActionObjectUI _actionObjectUI;

    [SerializeField] private UnityEvent _actions;
    [SerializeField] private UnityEvent _init;

    [SerializeField] private bool _isOnlyAction = false;

    private bool isTrigger = false;
    private bool isEnd = false;

    private void OnEnable()
    {
        _actionObjectUI.Deactive();
    }

    public void Enter()
    {
        isTrigger = true;
        _actionObjectUI.Init(PlayerInputSystemPC.main.InputButtons[0].KeyCode);
        _init?.Invoke();
    }

    public void Exit()
    {
        isTrigger = false;
        _actionObjectUI.Deactive();
    }

    public void Action()
    {
        if (isTrigger && isEnd == false)
        {
            _actions?.Invoke();
            if (_isOnlyAction)
            {
                isEnd = true;
                _actionObjectUI.Deactive();
            }
        }
    }
}
