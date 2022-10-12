using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObjUI : MonoBehaviour
{
    [SerializeField] private PullObjects _itemsPull;

    public void Init(Inventory inventory)
    {
        _itemsPull.Clear();

        for(int i = 0; i < inventory.KeyModels.Count; i++)
        {
            ItemPrefab itemUI = _itemsPull.AddObj() as ItemPrefab;
            itemUI.Init(inventory.KeyModels[i]);
        }
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            ItemPrefab itemUI = _itemsPull.AddObj() as ItemPrefab;
            itemUI.Init(inventory.Items[i]);
        }
    }
}
