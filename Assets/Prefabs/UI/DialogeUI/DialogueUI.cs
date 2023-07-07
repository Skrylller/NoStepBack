using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : UIWindow
{
    [SerializeField] private TextMeshProUGUI _characterName;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _characterIcon;

    private DialogueModel _dialogueModel;

    public void Init(DialogueModel dialogueModel)
    {
        _dialogueModel = dialogueModel;
        _characterName.text = _dialogueModel.Character.Name.GetLocalization();
    }

    public void UpdateUI()
    {
    }
}
