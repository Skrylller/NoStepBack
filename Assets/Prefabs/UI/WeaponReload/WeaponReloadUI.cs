using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponReloadUI : MonoBehaviour
{
    public enum AmmoState
    {
        have,
        havent,
    }

    [SerializeField] private List<AmmoFromBelt> _ammo = new List<AmmoFromBelt>();
    [SerializeField] private List<AmmoSlot> _ammoSlotsPistol = new List<AmmoSlot>();
    [SerializeField] private List<AmmoSlot> _ammoSlotsRifle = new List<AmmoSlot>();
    [SerializeField] private ModeSwitcher _weaponSwitcher;

    private WeaponModel _weapon;

    public Action OnClose;

    private void Start()
    {
        for (int i = 0; i < _ammo.Count; i++)
        {
            _ammo[i].OnReload += Reload;
            _ammo[i].OnAmmoCheck += AmmoCheck;
        }
    }

    private void OnDisable()
    {
        OnClose?.Invoke();
        OnClose = null;

        for (int i = 0; i < _ammo.Count; i++)
        {
            _ammo[i].MouseUp();
        }
    }

    public void Init(WeaponModel weapon, Action onEndReload)
    {
        _weapon = weapon;

        OnClose += onEndReload;

        if (weapon.Weapon == WeaponModel.WeaponType.None)
        {
            OnClose?.Invoke();
            OnClose = null;
            Close();
            return;
        }

        for(int i = 0; i < _ammo.Count; i++)
        {
            if (PlayerInventory.main.CheckItem(weapon.Bullet.BulletType, (uint)i + 1, false))
                _ammo[i].SetState((int)AmmoState.have);
            else
                _ammo[i].SetState((int)AmmoState.havent);
        }



        InitWeapon(weapon);
    }

    public void Close()
    {
        UIController.main.CloseAllWindow();
    }

    private void InitWeapon(WeaponModel weapon)
    {
        List<AmmoSlot> ammoInClipSlots = null;

        if (weapon.Weapon == WeaponModel.WeaponType.Pistol)
        {
            ammoInClipSlots = _ammoSlotsPistol;
        }
        else if (weapon.Weapon == WeaponModel.WeaponType.Rifle)
        {
            ammoInClipSlots = _ammoSlotsRifle;
        }

        _weaponSwitcher.State = (int)weapon.Weapon;

        for (int i = 0; i < ammoInClipSlots.Count; i++)
        {
            if (weapon.BulletInClip > i)
                ammoInClipSlots[i].SetState((int)AmmoState.have);
            else
                ammoInClipSlots[i].SetState((int)AmmoState.havent);
        }
            
    }

    public bool AmmoCheck()
    {
        return PlayerInventory.main.CheckItem(_weapon.Bullet.BulletType, 1, true);
    }

    private void Reload()
    {
        _weapon.BulletInClip++;
    }
}
