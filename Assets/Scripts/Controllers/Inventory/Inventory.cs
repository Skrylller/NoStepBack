using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItemCounter> _Items;
    [SerializeField] private List<KeyModel> _keyModels;
    protected virtual bool isPlayerInventory() { return false; }
    public List<InventoryItemCounter> Items { get { return _Items; } }
    public List<KeyModel> KeyModels { get { return _keyModels; } }

    public Action OnUpdate;

    public void AddItem(ItemModel item, uint value = 1)
    {
        if (item as KeyModel)
        {
            _keyModels.Add(item as KeyModel);

            if (isPlayerInventory())
                UIController.main.MessageUI.AddItem(item as KeyModel);
        }
        else
        {
            List<InventoryItemCounter> items = _Items.Where(x => x.ItemModel.Item == item.Item).ToList();

            if (items.Count > 0)
            {
                items.First().Count += value;
            }
            else
            {
                _Items.Add(new InventoryItemCounter(item, value));
                OnUpdate?.Invoke();
            }

            if(isPlayerInventory())
                UIController.main.MessageUI.AddItem(new InventoryItemCounter(item, value));
        }
    }

    public InventoryItem GetInventoryItem(ItemModel.ItemType item)
    {
        List<InventoryItemCounter> items = _Items.Where(x => x.ItemModel.Item == item).ToList();

        if (items.Count == 0)
            return null;

        return items.First();
    }

    public bool CheckItem(ItemModel.ItemType item, uint value = 1, bool isDelete = false)
    {
        InventoryItemCounter itemInventory = GetInventoryItem(item) as InventoryItemCounter;

        if (itemInventory == null)
            return false;

        if (itemInventory.Count >= value)
        {
            if (isDelete)
            {
                itemInventory.Count -= value;
                if (itemInventory.Count == 0)
                {
                    _Items.Remove(itemInventory);
                    OnUpdate?.Invoke();
                }
            }

            return true;
        }
        else
        {
            return false;
        }

    }

    public bool CheckKey(KeyModel.KeyType key)
    {
        return _keyModels.Where(x => x.Key == key).ToList().Count > 0;
    }
}

[System.Serializable]
public class InventoryItem
{
    [SerializeField] protected ItemModel _itemModel;
    [SerializeField] protected uint _count;
    [SerializeField] public ItemModel ItemModel { get { return _itemModel; } }

    public Action OnChange;
    public uint GetCount { get { return _count; } }
}

[System.Serializable]
public class InventoryItemCounter : InventoryItem
{
    public InventoryItemCounter(ItemModel itemModel, uint count)
    {
        _itemModel = itemModel;
        this._count = count;
        OnChange?.Invoke();
    }

    public uint Count
    {
        get { return _count; }
        set
        {
            _count = value;
            OnChange?.Invoke();
        }
    }
}
