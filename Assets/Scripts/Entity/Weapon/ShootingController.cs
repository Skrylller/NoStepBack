using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private PullObjects _bulletPull;
    [SerializeField] private PullObjects _particlePull;
    [SerializeField] private WeaponModel _weapon;

    [SerializeField] private Inventory _inventory;

    private bool delay;

    public void Shoot()
    {
        if (delay)
            return;

        if (_inventory.CheckItem(_weapon.Bullet.BulletType, 1, true))
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
        bullet.Init(_weapon.Bullet, _particlePull, Random.Range(-(_weapon.SpreadAngle / 2), _weapon.SpreadAngle / 2));
    }

    private IEnumerator WeaponDelayCourotine()
    {
        delay = true;
        yield return new WaitForSeconds(_weapon.ShootDelay);
        delay = false;
    }
}
