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
    [SerializeField] private PullObjects _choisesPull;
    [SerializeField] private GameObject ChoiseWindow;

    private DialogueModel _dialogueModel;

    private int _dCounter;

    public void Init(DialogueModel dialogueModel)
    {
        if(dialogueModel == null)
        {
            UIController.main.CloseAllWindow();
            return;
        }

        _dCounter = 0;
        _dialogueModel = dialogueModel;
        _characterName.text = _dialogueModel.Character.Name.GetLocalization();
        NextDialogue();
    }
    
    public void NextDialogue()
    {
        if (!gameObject.activeSelf)
            return;

        DialogueElement dialogueElement = _dialogueModel.GetDialogueElement(_dCounter);

        if (dialogueElement == null)
        {
            List<ChoiseElement> choises = _dialogueModel.GetChoiseList();
            if (choises.Count > 0)
            {
                return;
            }

            Init(_dialogueModel.GetNextDialogue());
            return;
        }

        ChoiseWindow.SetActive(false);

        _text.text = dialogueElement.Text.GetLocalization();
        _characterIcon.sprite = _dialogueModel.Character.GetEmotionIcon(dialogueElement.Emotion);

        if(dialogueElement.PlusItem.GetCount > 0)
            PlayerInventory.Inventory.AddItem(dialogueElement.PlusItem.ItemModel, dialogueElement.PlusItem.GetCount);

        if (dialogueElement.MinusItem.GetCount > 0)
            PlayerInventory.Inventory.CheckItem(dialogueElement.PlusItem.ItemModel.Item, dialogueElement.PlusItem.GetCount, true);

        _dCounter++;

        if(_dialogueModel.GetDialogueElement(_dCounter) == null)
        {
            List<ChoiseElement> choises = _dialogueModel.GetChoiseList();
            if (choises.Count > 0)
            {
                ShowChoises(choises);
                return;
            }
        }
    }

    public void Choise(int num)
    {
        ChoiseWindow.SetActive(false);

        Init(_dialogueModel.GetChoiseList()[num].Dialogue);
    }

    private void ShowChoises(List<ChoiseElement> choises)
    {
        ChoiseWindow.SetActive(true);

        _choisesPull.Clear();

        for (int i = 0; i < choises.Count; i++)
        {
            DialogueChoiseButton button = _choisesPull.AddObj() as DialogueChoiseButton;
            button.Init(i, choises[i].Text.GetLocalization(), Choise);
        }
    }
}
