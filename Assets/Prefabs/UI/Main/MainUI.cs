using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] private List<ItemModel.ItemType> items;
    [SerializeField] private ShootingController _shootingController;

    [SerializeField] private PullObjects _pull;
    [SerializeField] private ModeSwitcher _weaponIndikator;

    private void Start()
    {
        SetItems();
        _shootingController.OnChangeWeapon += SetWeapon;
    }

    private void OnEnable()
    {
        PlayerInventory.main.OnUpdate += SetItems;
    }

    private void OnDisable()
    {
        PlayerInventory.main.OnUpdate -= SetItems;
    }

    public void SetItems()
    {
        _pull.Clear();

        for(int i = 0; i < items.Count; i++)
        {
            InventoryItem item = PlayerInventory.main.GetInventoryItem(items[i]);
            if(item == null)
                continue;

            ItemPrefab itemPrefab = _pull.AddObj() as ItemPrefab;
            itemPrefab.Init(item);
        }
    }

    public void SetWeapon(WeaponModel.WeaponType weapon)
    {
        if (weapon == WeaponModel.WeaponType.None && PlayerInventory.main.CheckItem(ItemModel.ItemType.Flashlight))
            _weaponIndikator.State = 0;
        else
            _weaponIndikator.State = (int)weapon + 1;
    }
}
