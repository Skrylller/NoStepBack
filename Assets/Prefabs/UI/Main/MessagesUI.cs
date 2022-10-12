using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesUI : MonoBehaviour
{
    [SerializeField] private PullObjects _itemPlusMessages;
    [SerializeField] private PullObjects _messages;

    public void AddItem(InventoryItem item)
    {
        ItemPrefab itemText = _itemPlusMessages.AddObj() as ItemPrefab;
        itemText.Init(item);
    }
    public void AddItem(KeyModel key)
    {
        ItemPrefab itemText = _itemPlusMessages.AddObj() as ItemPrefab;
        itemText.Init(key);
    }
}
