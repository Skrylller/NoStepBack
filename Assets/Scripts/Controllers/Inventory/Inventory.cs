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

    public void Clear()
    {
        Items.Clear();
        KeyModels.Clear();
        WeaponModels.Clear();
        NoteModels.Clear();

        OnUpdate?.Invoke();
    }

    /// <summary>
    /// Добавить элемент инвентаря (любой тип)
    /// </summary>
    /// <param name="item"></param>
    /// <param name="value"></param>
    public void AddItem(ItemModel item, uint value = 1)
    {
        if (item as KeyModel)
        {
            KeyModel key = item as KeyModel;
            _keyModels.Add(key);

            if (isPlayerInventory)
            {
                UIController.main.MessageUI.AddItem(item as KeyModel);

                if(DaysController.main.IsDay)
                    SaveItem(key.Key, 1);
            }
        }
        else if (item as WeaponModel)
        {
            WeaponModel weapon = item as WeaponModel;
            _weaponModels.Add(weapon);

            if (isPlayerInventory)
            {
                UIController.main.MessageUI.AddItem(item as WeaponModel);

                if (DaysController.main.IsDay)
                    SaveItem(weapon.Weapon, 1);
            }
        }
        else if (item as NoteModel)
        {
            NoteModel note = item as NoteModel;
            _noteModels.Add(note);

            if (isPlayerInventory)
            {
                UIController.main.OpenNoteUI(item as NoteModel);
                UIController.main.MessageUI.AddItem(item as NoteModel);

                if (DaysController.main.IsDay)
                    SaveItem(note.NoteType, 1);
            }
        }
        else
        {
            List<InventoryItemCounter> items = _Items.Where(x => x.ItemModel.Item == item.Item).ToList();

            if (items.Count > 0)
            {
                items.First().Count += value;

                if (isPlayerInventory && DaysController.main.IsDay)
                    SaveItem(items.First().ItemModel.Item, (int)items.First().GetCount);
            }
            else
            {
                _Items.Add(new InventoryItemCounter(item, value));
                OnUpdate?.Invoke();

                if (isPlayerInventory && DaysController.main.IsDay)
                    SaveItem(_Items.Last().ItemModel.Item, (int)_Items.Last().GetCount);
            }

            if (isPlayerInventory)
            {
                UIController.main.MessageUI.AddInventoryItem(new InventoryItemCounter(item, value));
            }
        }
    }

    /// <summary>
    /// Просто возвращает предмет инвентаря (ItemModel)
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public InventoryItem GetInventoryItem(ItemModel.ItemType item)
    {
        List<InventoryItemCounter> items = _Items.Where(x => x.ItemModel.Item == item).ToList();

        if (items.Count == 0)
            return null;

        return items.First();
    }

    /// <summary>
    /// Проверяет наличие предмета (ItemModel)
    /// </summary>
    /// <param name="item"></param>
    /// <param name="value"></param>
    /// <param name="isDelete"></param>
    /// <returns></returns>
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

                if (isPlayerInventory && DaysController.main.IsDay)
                {
                    SaveItem(item, (int)itemInventory.Count);
                }

                if (itemInventory.Count == 0)
                {
                    _Items.Remove(itemInventory);
                    OnUpdate?.Invoke();
                    
                    if (isPlayerInventory && DaysController.main.IsDay)
                    {
                        SaveItem(item, 0);
                    }
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

    public void SaveItem(Enum item, int count)
    {
        DataController.main.SaveItem(item, count);
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
