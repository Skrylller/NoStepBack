using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private PullObjects _bulletPull;
    [SerializeField] private PullObjects _particlePull;
    [SerializeField] private WeaponModel _weapon;
    [SerializeField] private ModeSwitcher _handSwitcher;

    public Action<WeaponModel.WeaponType> OnChangeWeapon;

    private bool delay;

    public void Start()
    {
        SetWeapon(_weapon.Weapon);
    }

    public void SetWeapon(WeaponModel.WeaponType weapon)
    {
        if(weapon == WeaponModel.WeaponType.None)
        {
            _weapon = null;
            _handSwitcher.State = (int)weapon;
            OnChangeWeapon?.Invoke(weapon);
            return;
        }

        WeaponModel tWeapon = PlayerInventory.main.GetWeapon(weapon);

        if (tWeapon == null)
        {
            if (_weapon != null && weapon == _weapon.Weapon)
                SetWeapon(WeaponModel.WeaponType.None);
            return;
        }

        _weapon = tWeapon;

        _handSwitcher.State = (int)weapon;
        
        OnChangeWeapon?.Invoke(weapon);
    }

    public void Shoot()
    {
        if (delay || _weapon == null)
            return;

        if (PlayerInventory.main.CheckItem(_weapon.Bullet.BulletType, 1, true))
        {
            for (int i = 0; i < _weapon.BulletÑount; i++)
            {
                CreateBullet();
            }

            StartCoroutine(WeaponDelayCourotine());
        }
    }

    private void CreateBullet()
    {
        BulletEntity bullet = _bulletPull.AddObj() as BulletEntity;
        bullet.Init(_weapon.Bullet, _particlePull, UnityEngine.Random.Range(-(_weapon.SpreadAngle / 2), _weapon.SpreadAngle / 2));
    }

    private IEnumerator WeaponDelayCourotine()
    {
        delay = true;
        yield return new WaitForSeconds(_weapon.ShootDelay);
        delay = false;
    }
}
