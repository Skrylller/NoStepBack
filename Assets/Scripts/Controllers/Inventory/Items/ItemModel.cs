using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items/Item")]
public class ItemModel : ScriptableObject
{
    public enum ItemType
    {
        PistolAmmo,
        RifleAmmo,
        MedKit,
        MasterKey,
        Key,
        Weapon,
        Flashlight
    }

    [Header("Item")]
    [SerializeField] private ItemType _item;

    [SerializeField] private string _name;

    [SerializeField] private Sprite _icon;

    public ItemType Item { get { return _item; } }
    public string Name { get { return _name; } }
    public Sprite Icon { get { return _icon; } }

}
