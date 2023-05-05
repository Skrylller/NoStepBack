using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory main;
    public static Inventory Inventory;

    [SerializeField] private Inventory _inventory;

    [SerializeField] private PullableObj _dropObject;

    [SerializeField] private Vector2 _dropRandomDirectional;

    private void Awake()
    {
        main = this;
        Inventory = _inventory;
    }

    public void DropItem(ItemModel item, Transform dropPosition, uint count = 1)
    {
        if(!Inventory.CheckItem(item.Item, 1, true))
        {
            return;
        }

        DropObject dropItem = PullsController.main.GetPull(_dropObject).AddObj() as DropObject;

        List<InventoryItemCounter> itemList = new List<InventoryItemCounter>();
        itemList.Add(new InventoryItemCounter(item, count));

        Inventory dropInventory = new Inventory(itemList);

        Vector2 directional = new Vector2(Random.Range(-_dropRandomDirectional.x, _dropRandomDirectional.x), Random.Range(0, _dropRandomDirectional.y * 2));

        dropItem.Init(dropInventory, dropPosition.position, directional);
    }
}
