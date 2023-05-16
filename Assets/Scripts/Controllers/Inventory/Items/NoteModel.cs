using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NAME_NOTE", menuName = "ScriptableObjects/Note")]
public class NoteModel : ItemModel
{
    public enum Note
    {
        TestNote,
    }

    [Header("Note")]
    [SerializeField] private Note _noteType;
    [SerializeField] private LocalizationData _text;

    public Note NoteType => _noteType;
    public string Text => _text.GetLocalization(Localizator.main.SelectedLaunguage);
}
