using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Items/Weapon")]
public class WeaponModel : ItemModel
{
    public enum WeaponType
    {
        None,
        Pistol,
        Rifle
    }

    [Header("Weapon")]
    [SerializeField] private BulletModel _bullet;
    [SerializeField] private WeaponType _weapon;
    [SerializeField] private uint _bulletInClipMax;
    [SerializeField] private float _shootDelay;
    [SerializeField] private uint _bullet—ountToShoot;
    [Range(0,360)]
    [SerializeField] private float _spreadAngle;

    public uint BulletInClip = 0;

    public BulletModel Bullet { get { return _bullet; } }
    public WeaponType Weapon { get { return _weapon; } }
    public uint BullietInClipMax { get { return _bulletInClipMax; } }
    public float ShootDelay { get { return _shootDelay; } }
    public uint Bullet—ount { get { return _bullet—ountToShoot; } }
    public float SpreadAngle { get { return _spreadAngle; } }
}
