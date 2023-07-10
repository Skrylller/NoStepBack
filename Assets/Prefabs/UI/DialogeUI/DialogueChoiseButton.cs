using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueChoiseButton : PullableObj
{
    [SerializeField] private TextMeshProUGUI _text;

    private Action<int> OnPress;
    private int _choiseNum;

    public void Init(int num, string text, Action<int> action)
    {
        _choiseNum = num;
        _text.text = text;
        OnPress = action;
    }

    public void Press()
    {
        OnPress?.Invoke(_choiseNum);
    }
}
