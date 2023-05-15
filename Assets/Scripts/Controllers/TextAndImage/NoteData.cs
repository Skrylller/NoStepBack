using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NOTE_NAME", menuName = "ScriptableObjects/Note")]
public class NoteData : ScriptableObject
{
    [SerializeField] private LocalizationData _name;
    [SerializeField] private LocalizationData _title;
    [SerializeField] private LocalizationData _text;

    public LocalizationData Name => _name;
    public LocalizationData Title => _title;
    public LocalizationData Text => _text;

}
