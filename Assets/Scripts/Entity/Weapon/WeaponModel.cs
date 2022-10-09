using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapons/Weapon")]
public class WeaponModel : ScriptableObject
{
    [SerializeField] private BulletModel _bullet;
    [SerializeField] private uint _bulletInClip;
    [SerializeField] private float _shootDelay;
    [SerializeField] private uint _bullet—ountToShoot;
    [Range(0,360)]
    [SerializeField] private float _spreadAngle;

    public BulletModel Bullet { get { return _bullet; } }
    public uint BullietInClip { get { return _bulletInClip; } }
    public float ShootDelay { get { return _shootDelay; } }
    public uint Bullet—ount { get { return _bullet—ountToShoot; } }
    public float SpreadAngle { get { return _spreadAngle; } }
}
