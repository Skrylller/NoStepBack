using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] private List<ItemModel.ItemType> items;
    [SerializeField] private PullObjects _pull;

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        PlayerInventory.main.OnUpdate += Init;
    }

    private void OnDisable()
    {
        PlayerInventory.main.OnUpdate -= Init;
    }

    public void Init()
    {
        _pull.Clear();

        for(int i = 0; i < items.Count; i++)
        {
            InventoryItem item = PlayerInventory.main.GetInventoryItem(items[i]);
            if(item == null)
                continue;

            ItemPrefab itemPrefab = _pull.AddObj() as ItemPrefab;
            itemPrefab.Init(item);
        }
    }
}
