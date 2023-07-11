using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataController : MonoBehaviour
{
    public static DataController main;

    public enum DataTypeBool
    {
        startGame,
        isDay,
    }
    public enum DataTypeInt
    {
        days,
    }

    public List<ItemModel> _allItems = new List<ItemModel>();
    public List<KeyModel> _allKeys = new List<KeyModel>();
    public List<WeaponModel> _allWeapons = new List<WeaponModel>();
    public List<NoteModel> _allNotes = new List<NoteModel>();

    public Action OnSave;
    public Action OnLoad;

    private const string _inventoryKey = "Inventory_";

    private void Awake()
    {
        main = this;
    }

    public void Save()
    {
        OnSave?.Invoke();
    }

    public void Load()
    {
        OnLoad?.Invoke();
    }

    public void SaveFlag(DataTypeBool dataTypeBool, bool data)
    {
        PlayerPrefs.SetInt(dataTypeBool.ToString(), data ? 1 : 0);
    }

    public void SaveFlag(DataTypeInt dataTypeInt, int data)
    {
        PlayerPrefs.SetInt(dataTypeInt.ToString(), data);
    }

    public void SaveItem(ItemModel itemModel, int count = 0)
    {
        PlayerPrefs.SetInt(_inventoryKey+itemModel.Item.ToString(), count);
    }

    public bool LoadFlag(DataTypeBool dataType)
    {
        return PlayerPrefs.GetInt(dataType.ToString(), 0) > 0;
    }
    public int LoadFlag(DataTypeInt dataType)
    {
        return PlayerPrefs.GetInt(dataType.ToString(), 0);
    }

    public int LoadItem(Enum item)
    {
        return PlayerPrefs.GetInt(_inventoryKey + item.ToString(), 0);
    }

    public void LoadInventory(Inventory inventory)
    {
        inventory.Clear();

        for(int i = 0; i < Enum.GetNames(typeof(ItemModel.ItemType)).Length; i++)
        {
            inventory.AddItem(_allItems[i], (uint)LoadItem((ItemModel.ItemType)i));
        }

        for (int i = 0; i < Enum.GetNames(typeof(KeyModel.KeyType)).Length; i++)
        {
            inventory.AddItem(_allKeys[i], (uint)LoadItem((KeyModel.KeyType)i));
        }

        for (int i = 0; i < Enum.GetNames(typeof(WeaponModel.WeaponType)).Length; i++)
        {
            inventory.AddItem(_allWeapons[i], (uint)LoadItem((WeaponModel.WeaponType)i));
        }

        for (int i = 0; i < Enum.GetNames(typeof(NoteModel.Note)).Length; i++)
        {
            inventory.AddItem(_allNotes[i], (uint)LoadItem((NoteModel.Note)i));
        }
    }
}
