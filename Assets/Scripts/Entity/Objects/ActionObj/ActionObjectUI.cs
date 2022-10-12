using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionObjectUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _buttonText;

    public void Init(KeyCode key)
    {
        gameObject.SetActive(true);
        _buttonText.text = key.ToString();
    }

    public void Deactive()
    {
        gameObject.SetActive(false);
    }
}
