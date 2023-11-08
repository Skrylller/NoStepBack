using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionObject : MonoBehaviour, DaysUpdaterObject
{
    [SerializeField] private BoxCollider2D _areaAction;
    [SerializeField] private ActionObjectUI _actionObjectUI;

    [SerializeField] private UnityEvent _actions;
    [SerializeField] private UnityEvent _init;

    [SerializeField] private bool _isOnlyAction = false;

    [SerializeField] private List<KeyModel> keys = new List<KeyModel>();

    private bool isTrigger = false;
    private bool isEnd = false;

    private void OnEnable()
    {
        _actionObjectUI.Deactive();
    }

    public void Enter()
    {
        if (isEnd == false)
        {
            isTrigger = true;
            _actionObjectUI.Init(PlayerInputSystemPC.main.InputButtons[0].KeyCode, keys);
            _init?.Invoke();
        }
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
            if (!CheckKeys())
            {
                _actionObjectUI.HaventKeys();
                return;
            }

            _actions?.Invoke();
            if (_isOnlyAction)
            {
                isEnd = true;
                _actionObjectUI.Deactive();
            }
        }
    }

    private bool CheckKeys()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (!PlayerInventory.Inventory.CheckKey(keys[i].Key))
                return false;
                
        }
        return true;
    }

    public void DayUpdate()
    {
        isEnd = false;
    }

    public void NightUpdate()
    {
        isEnd = false;
    }
}
