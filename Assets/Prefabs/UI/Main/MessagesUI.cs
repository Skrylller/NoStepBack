using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesUI : MonoBehaviour
{
    [SerializeField] private PullObjects _itemPlusMessages;
    [SerializeField] private PullObjects _messages;

    public void AddInventoryItem(InventoryItem item)
    {
        ItemPrefab itemText = _itemPlusMessages.AddObj() as ItemPrefab;
        itemText.InitWithCounter(item);
    }
    public void AddItem(ItemModel item)
    {
        ItemPrefab itemText = _itemPlusMessages.AddObj() as ItemPrefab;
        itemText.Init(item);
    }

    public void Send(string str)
    {

    }
}
