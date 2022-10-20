using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionObjectUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private PullObjects _keysPull;

    public void Init(KeyCode key, List<KeyModel> keys)
    {
        gameObject.SetActive(true);
        _buttonText.text = key.ToString();

        _keysPull.Clear();
        if(keys.Count == 0)
        {
            _keysPull.gameObject.SetActive(false);
        }

        for (int i = 0; i < keys.Count; i++)
        {
            ItemPrefab item = _keysPull.AddObj() as ItemPrefab;
            item.Init(keys[i]);
        }
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }

    public void HaventKeys()
    {
    }
}
