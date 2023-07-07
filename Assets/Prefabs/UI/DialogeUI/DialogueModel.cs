using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueModel : ScriptableObject
{
    [SerializeField] private CharacterModel _character;

    [SerializeField] private List<DialogueElement> _dialogueElements = new List<DialogueElement>();
    [SerializeField] private List<ChoiseElement> _choiseElement = new List<ChoiseElement>();

    [SerializeField] private DialogueModel _nextDialogue;

    private int _dialogueNum;

    public CharacterModel Character => _character;

    public DialogueElement GetDialogueElement(int counter)
    {
        return _dialogueElements[counter];
    }

    public DialogueModel InputChoise(int num)
    {
        return _choiseElement[num].Dialogue;
    }

    public DialogueModel GetNextDialogue()
    {
        return _nextDialogue;
    }
}

[System.Serializable]
public class DialogueElement
{
    [SerializeField] public LocalizationData Text;
    [SerializeField] public CharacterModel.CharacterEmotionsType Emotion;
    [SerializeField] public ItemModel PlusItem;
    [SerializeField] public ItemModel MinusItem;
}

[System.Serializable]
public class ChoiseElement
{
    [SerializeField] public LocalizationData Text;
    [SerializeField] public DialogueModel Dialogue;
}
