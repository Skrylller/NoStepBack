using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    [SerializeField] private InventoryObjUI _inventoryObjUI;

    [SerializeField] private Inventory _inventory;
    public Inventory Inventory { get { return _inventory; } }

    private void OnEnable()
    {
        _inventoryObjUI.gameObject.SetActive(false);
    }

    public void Init()
    {
        _inventoryObjUI.Init(_inventory);
        _inventoryObjUI.gameObject.SetActive(true);
    }

    public void TakeItems()
    {
        for(int i = 0; i < _inventory.Items.Count; i++)
        {
            PlayerInventory.main.AddItem(_inventory.Items[i].ItemModel, _inventory.Items[i].Count);
        }

        for (int i = 0; i < _inventory.KeyModels.Count; i++)
        {
            PlayerInventory.main.AddItem(_inventory.KeyModels[i]);
        }

        for (int i = 0; i < _inventory.weaponModels.Count; i++)
        {
            PlayerInventory.main.AddItem(_inventory.weaponModels[i]);
        }
    }
}
