using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue/Dialogue")]
public class DialogueModel : ScriptableObject
{
    [SerializeField] private CharacterModel _character;

    [SerializeField] private List<DialogueElement> _dialogueElements = new List<DialogueElement>();
    [SerializeField] private List<ChoiseElement> _choiseElement = new List<ChoiseElement>();

    [SerializeField] private DialogueModel _nextDialogue;

    public CharacterModel Character => _character;

    public DialogueElement GetDialogueElement(int counter)
    {
        if (_dialogueElements.Count > counter)
            return _dialogueElements[counter];
        else
            return null;
    }

    public List<ChoiseElement> GetChoiseList()
    {
        return _choiseElement;
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
    [SerializeField] public InventoryItem PlusItem;
    [SerializeField] public InventoryItem MinusItem;
}

[System.Serializable]
public class ChoiseElement
{
    [SerializeField] public LocalizationData Text;
    [SerializeField] public DialogueModel Dialogue;
}
