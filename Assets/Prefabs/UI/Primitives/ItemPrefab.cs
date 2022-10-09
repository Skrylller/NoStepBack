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

    public void OnEnable()
    {
        if (_item != null)
            _item.OnChange += SetData;
    }

    public void OnDisable()
    {
        if (_item != null)
            _item.OnChange -= SetData;
    }

    public void Init(InventoryItem item)
    {
        if (_item != null)
            _item.OnChange -= SetData;

        _item = item;
        SetData();

        _item.OnChange += SetData;
    }
    public void Init(KeyModel key)
    {
        if (_icon != null)
            _icon.sprite = key.Icon;

        if (_count != null)
            _count.text = "1";

        if (_itemName != null)
            _itemName.text = key.name;
    }

    private void SetData()
    {
        if(_icon != null)
            _icon.sprite = _item.ItemModel.Icon;

        if(_count != null)
            _count.text = _item.GetCount.ToString();

        if(_itemName != null)
            _itemName.text = _item.ItemModel.name;
    }
}
