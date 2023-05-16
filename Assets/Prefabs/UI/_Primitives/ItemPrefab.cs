using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPrefab : PullableObj
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private TextMeshProUGUI _itemName;

    private InventoryItem _item;
    private ItemModel _itemModel;

    public void OnEnable()
    {
        if (_item != null)
        {
            Localizator.main.OnChangeLaunguage += SetLaunguage;
            _item.OnChange += SetData;
        }

    }

    public void OnDisable()
    {
        if (_item != null)
        {
            _item.OnChange -= SetData;
            Localizator.main.OnChangeLaunguage -= SetLaunguage;
        }

    }

    public void InitWithCounter(InventoryItem item)
    {
        if (_item != null)
            _item.OnChange -= SetData;

        _item = item;
        _itemModel = item.ItemModel;

        SetData();

        _item.OnChange += SetData;
    }
    public void Init(ItemModel item)
    {
        if(_itemModel == null)
            Localizator.main.OnChangeLaunguage += SetLaunguage;

        _itemModel = item;

        if (_icon != null)
            _icon.sprite = item.Icon;

        SetName();

        if (_count != null)
            _count.text = "1";
    }

    private void SetLaunguage(Localizator.Launguage launguage)
    {
        SetName();
    }

    private void SetName()
    {
        if (_itemName != null && _itemModel != null)
            _itemName.text = _itemModel.Name;
    }

    private void SetData()
    {
        if(_icon != null)
            _icon.sprite = _item.ItemModel.Icon;

        if(_count != null)
            _count.text = _item.GetCount.ToString();

        SetName();
    }
}
