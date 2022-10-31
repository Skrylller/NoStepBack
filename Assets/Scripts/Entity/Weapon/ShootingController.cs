using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public enum HandState
    {
        idle,
        shoot
    }

    [SerializeField] private WeaponModel _weapon;
    [SerializeField] private Transform _bulletDefPos;
    [SerializeField] private ModeSwitcher _handSwitcher;
    [SerializeField] private Animator _handAnimator;

    public Action<WeaponModel.WeaponType> OnChangeWeapon;

    private bool delay;

    [Header("Particles")]
    [SerializeField] private PullableObj _shootPart;
    private PullObjects _partPullObjects;

    public void Start()
    {
        SetWeapon(_weapon.Weapon);
        _partPullObjects = PullsController.main.GetPull(_shootPart);
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
        _handAnimator.SetInteger("State", (int)HandState.shoot);
        BulletEntity bullet = PullsController.main.GetPull(_weapon.Bullet.BulletPref).AddObj() as BulletEntity;
        bullet.Init(_weapon.Bullet, UnityEngine.Random.Range(-(_weapon.SpreadAngle / 2), _weapon.SpreadAngle / 2));
        PullableObj part = _partPullObjects.AddObj();
        part.SetTransform(_bulletDefPos.transform.position, _bulletDefPos.transform.eulerAngles.z);
    }

    private IEnumerator WeaponDelayCourotine()
    {
        delay = true;
        yield return new WaitForSeconds(_weapon.ShootDelay);
        delay = false;
    }
}
