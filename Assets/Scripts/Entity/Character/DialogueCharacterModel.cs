using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueCharacter", menuName = "ScriptableObjects/Dialogue/DialogueCharacter")]
public class DialogueCharacterModel : ScriptableObject
{
    [SerializeField] private List<LocalizationData> _dialogueTexts;

    public int Pages => _dialogueTexts.Count;

    public LocalizationData GetDialogueText(int index)
    {
        return _dialogueTexts[index];
    }
}
