using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/Weapons/Bullet")]
public class BulletModel : ScriptableObject
{
    [SerializeField] private BulletEntity _bulletPref;
    [SerializeField] private float _speed;
    [SerializeField] private uint _dammage;
    [SerializeField] private float _lifeTime;
    [SerializeField] private ItemModel.ItemType _bulletType;
    [SerializeField] private bool _raycastPhysics;
    [SerializeField] private float _distance;
    [SerializeField] private float _distanceBackCheck;

    public BulletEntity BulletPref => _bulletPref;
    public float Speed { get { return _speed; } }
    public uint Dammage { get { return _dammage; } }
    public float LifeTime { get { return _lifeTime; } }
    public ItemModel.ItemType BulletType { get { return _bulletType; } }
    public bool RaycastPhysics => _raycastPhysics;
    public float Distance { get { return _distance; } }
    public float DistanceBackCheck { get { return _distanceBackCheck; } }
}
