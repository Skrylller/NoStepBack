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

    }
}
