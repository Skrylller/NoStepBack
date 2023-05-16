using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] private List<InventoryItemCounter> _Items = new List<InventoryItemCounter>();
    [SerializeField] private List<KeyModel> _keyModels = new List<KeyModel>();
    [SerializeField] private List<WeaponModel> _weaponModels = new List<WeaponModel>();
    [SerializeField] private List<NoteModel> _noteModels = new List<NoteModel>();
    public List<InventoryItemCounter> Items { get { return _Items; } }
    public List<KeyModel> KeyModels { get { return _keyModels; } }
    public List<WeaponModel> WeaponModels { get { return _weaponModels; } }
    public List<NoteModel> NoteModels { get { return _noteModels; } }

    public Action OnUpdate;

    [SerializeField] private bool isPlayerInventory = false;

    public Inventory(List<InventoryItemCounter> items)
    {
        _Items = items;

    }

    public void AddItem(ItemModel item, uint value = 1)
    {
        if (item as KeyModel)
        {
            _keyModels.Add(item as KeyModel);

            if (isPlayerInventory)
                UIController.main.MessageUI.AddItem(item as KeyModel);
        }
        else if (item as WeaponModel)
        {
            _weaponModels.Add(item as WeaponModel);

            if (isPlayerInventory)
                UIController.main.MessageUI.AddItem(item as WeaponModel);
        }
        else if (item as NoteModel)
        {
            _noteModels.Add(item as NoteModel);

            if (isPlayerInventory)
            {
                UIController.main.OpenNoteUI(item as NoteModel);
                UIController.main.MessageUI.AddItem(item as NoteModel);
            }
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

            if(isPlayerInventory)
                UIController.main.MessageUI.AddInventoryItem(new InventoryItemCounter(item, value));
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

    public WeaponModel GetWeapon(WeaponModel.WeaponType weapon)
    {
        List<WeaponModel> weapons = _weaponModels.Where(x => x.Weapon == weapon).ToList();

        return weapons.Count > 0 ? weapons[0] : null;
    }

    public bool CheckKey(KeyModel.KeyType key)
    {
        return _keyModels.Where(x => x.Key == key).ToList().Count > 0;
    }

    public NoteModel CheckNote(NoteModel.Note note)
    {
        return _noteModels.Where(x => x.NoteType == note).ToList().First();
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
