using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "ScriptableObjects/Items/Key")]
public class KeyModel : ItemModel
{
    public enum KeyType
    {
        Key
    }

    [Header("Key")]
    [SerializeField] private KeyType _key;

    public KeyType Key { get { return _key; } }
}
