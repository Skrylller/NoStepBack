using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

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
    public List<ItemModel> _allKeys = new List<ItemModel>();
    public List<ItemModel> _allWeapons = new List<ItemModel>();
    public List<ItemModel> _allNotes = new List<ItemModel>();

    public Action OnSave;
    public Action OnLoad;

    private const string _inventoryKey = "Inventory_";
    private const int _itemDummyZone = 3;
    private const int _weaponDummyZone = 3;

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

    public void RemoveAllData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SaveFlag(DataTypeBool dataTypeBool, bool data)
    {
        PlayerPrefs.SetInt(dataTypeBool.ToString(), data ? 1 : 0);
    }

    public void SaveFlag(DataTypeInt dataTypeInt, int data)
    {
        PlayerPrefs.SetInt(dataTypeInt.ToString(), data);
    }

    public void SaveItem(Enum item, int count = 0)
    {
        PlayerPrefs.SetInt(_inventoryKey+item.ToString(), count);
    }

    public bool LoadFlag(DataTypeBool dataType)
    {
        return PlayerPrefs.GetInt(dataType.ToString(), 0) > 0;
    }
    public int LoadFlag(DataTypeInt dataType)
    {
        return PlayerPrefs.GetInt(dataType.ToString(), 0);
    }

    public int LoadItem(string item)
    {
        return PlayerPrefs.GetInt(_inventoryKey + item.ToString(), 0);
    }

    public void LoadInventory(Inventory inventory)
    {
        inventory.Clear();

        LoadItemGroup<ItemModel.ItemType>(inventory, _allItems, _itemDummyZone);
        LoadItemGroup<KeyModel.KeyType>(inventory, _allKeys);
        LoadItemGroup<WeaponModel.WeaponType>(inventory, _allWeapons, _weaponDummyZone);
        LoadItemGroup<NoteModel.Note>(inventory, _allNotes);
    }

    public void ShowData()
    {
        StringBuilder message = new StringBuilder();

        message.Append(ShowItemDataGroup<ItemModel.ItemType>());
        message.Append(ShowItemDataGroup<KeyModel.KeyType>());
        message.Append(ShowItemDataGroup<WeaponModel.WeaponType>());
        message.Append(ShowItemDataGroup<NoteModel.Note>());

        foreach (DataTypeBool type in Enum.GetValues(typeof(DataTypeBool)))
        {
            message.Append($"{type} - {LoadFlag(type)}\n");
        }

        message.Append("\n");

        foreach (DataTypeInt type in Enum.GetValues(typeof(DataTypeInt)))
        {
            message.Append($"{type} - {LoadFlag(type)}\n");
        }

        Debug.Log(message);
    }

    private StringBuilder ShowItemDataGroup<T1>()
    {
        StringBuilder message = new StringBuilder();

        for (int i = 0; i < Enum.GetNames(typeof(T1)).Length; i++)
        {
            if(LoadItem(Enum.GetNames(typeof(T1))[i]) > 1)
                message.Append($"{Enum.GetNames(typeof(T1))[i]} - {LoadItem(Enum.GetNames(typeof(T1))[i])}\n");
        }

        message.Append("\n");

        return message;
    }

    private void LoadItemGroup<T1>(Inventory inventory, List<ItemModel> items, int dummyZone = 0)
    {
        for (int i = 0; i < Enum.GetNames(typeof(T1)).Length - dummyZone; i++)
        {
            uint count = (uint)LoadItem(Enum.GetNames(typeof(T1))[i + dummyZone]);
            if(count > 0)
                inventory.AddItem(items[i], count);
        }
    }
}
