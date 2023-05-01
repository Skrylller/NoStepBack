using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIController : MonoBehaviour
{
    public static UIController main;

    [SerializeField] private MessagesUI _messageUI;
    [SerializeField] private MainUI _mainUI;
    [SerializeField] private WeaponReloadUI _weaponReloadUI;
    public MessagesUI MessageUI { get { return _messageUI; } }


    private void Awake()
    {
        main = this;
    }

    public void PressToEsc()
    {
        if (_mainUI.gameObject.activeSelf)
            OpenMenu();
        else
            CloseAllWindow();
    }

    public void OpenMenu()
    {

    }

    public void CloseAllWindow()
    {
        _mainUI.gameObject.SetActive(true);
        _weaponReloadUI.gameObject.SetActive(false);
    }

    public void OpenWeaponReloadUI(WeaponModel weapon, Action onEndReload)
    {
        CloseAllWindow();

        _weaponReloadUI.gameObject.SetActive(true);
        _weaponReloadUI.Init(weapon, onEndReload);
    }
}
