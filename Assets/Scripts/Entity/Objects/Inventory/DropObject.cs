using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : PullableObj
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private InventoryObject _inventoryObj;
    [SerializeField] private ActionObject _actionObj;

    public void Init(Inventory items)
    {
        _inventoryObj.SetInventory(items);
    }

    public void Init(Inventory items, Vector2 directional)
    {
        Init(items);
        StartImpulse(directional);
    }

    private void StartImpulse(Vector2 directional)
    {
        _rigidbody2D.AddForce(directional);
    }
}
