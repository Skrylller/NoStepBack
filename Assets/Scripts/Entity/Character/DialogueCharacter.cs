using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCharacter : MonoBehaviour
{
    [SerializeField] private DialogueModel _dialogueModel;
    [SerializeField] private DialogueCharacterModel _dialogueCharacterModel;
    [SerializeField] private DialogueCharacterUI _dialogueCharacterUI;

    private int page = 0;

    public void ShowDialogue()
    {
        if(_dialogueModel != null)
        {
            UIController.main.OpenDialogueUI(_dialogueModel);
            return;
        }

        if (_dialogueCharacterModel == null)
            return;

        if (page >= _dialogueCharacterModel.Pages)
            page = 0;

        _dialogueCharacterUI.ShowDialogue(_dialogueCharacterModel.GetDialogueText(page), page == _dialogueCharacterModel.Pages - 1);
        page++;
    }
}
