using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController main;

    [SerializeField] private MessagesUI _messageUI;
    public MessagesUI MessageUI { get { return _messageUI; } }


    private void Awake()
    {
        main = this;
    }
}
