using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items/Item")]
public class ItemModel : ScriptableObject
{
    public enum ItemType
    {
        Key,
        Weapon,
        Note,
        PistolAmmo,
        RifleAmmo,
        MedKit,
        MasterKey,
        Flashlight,
    }

    [Header("Item")]
    [SerializeField] private ItemType _item;

    [SerializeField] private LocalizationData _name;

    [SerializeField] private LocalizationData _description;

    [SerializeField] private Sprite _icon;

    public ItemType Item { get { return _item; } }
    public string Name { get { return _name.GetLocalization(Localizator.main.SelectedLaunguage); } }
    public string Description { get { return _description.GetLocalization(Localizator.main.SelectedLaunguage); } }
    public Sprite Icon { get { return _icon; } }

}
