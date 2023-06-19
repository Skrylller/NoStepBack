using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCharacter : MonoBehaviour
{
    [SerializeField] private DialogueCharacterModel _dialogue;
    [SerializeField] private DialogueCharacterUI _dialogueCharacterUI;

    private int page = 0;

    public void ShowDialogue()
    {
        if (page >= _dialogue.Pages)
            page = 0;

        _dialogueCharacterUI.ShowDialogue(_dialogue.GetDialogueText(page), page == _dialogue.Pages - 1);
        page++;
    }
}
