using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] private List<ItemModel.ItemType> items;
    [SerializeField] private PullObjects _pull;
    [SerializeField] private Inventory _inventory;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _inventory.OnUpdate += Init;
    }

    private void OnDisable()
    {
        _inventory.OnUpdate -= Init;
    }

    public void Init()
    {
        _pull.Clear();

        for(int i = 0; i < items.Count; i++)
        {
            InventoryItem item = _inventory.GetInventoryItem(items[i]);
            if(item == null)
                continue;

            ItemPrefab itemPrefab = _pull.AddObj() as ItemPrefab;
            itemPrefab.Init(item);
        }
    }
}
