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
    [SerializeField] private NotePanelUI _notePanelUI;
    public MessagesUI MessageUI { get { return _messageUI; } }

    private bool isOpenedWindow;

    private void Awake()
    {
        main = this;
    }

    public void PressToEsc()
    {
        if (isOpenedWindow)
            CloseAllWindow();
        else
            OpenMenu();
    }

    public void OpenMenu()
    {
        
    }

    public void CloseAllWindow()
    {
        _mainUI.gameObject.SetActive(true);
        _weaponReloadUI.gameObject.SetActive(false);
        _notePanelUI.gameObject.SetActive(false);

        Bootastrap.main.Player.isBusy = false;

        isOpenedWindow = false;
    }

    public void OpenWeaponReloadUI(WeaponModel weapon, Transform dropPosition)
    {
        if (_weaponReloadUI.gameObject.activeSelf)
            return;

        CloseAllWindow();

        PlayerBusy();

        _weaponReloadUI.gameObject.SetActive(true);
        _weaponReloadUI.Init(weapon, dropPosition);

        isOpenedWindow = true;
    }

    public void OpenNoteUI(NoteModel note)
    {
        CloseAllWindow();

        PlayerBusy();

        _notePanelUI.gameObject.SetActive(true);
        _notePanelUI.Init(note);

        isOpenedWindow = true;
    }

    private void PlayerBusy()
    {
        Bootastrap.main.Player.isBusy = true;
    }
}
