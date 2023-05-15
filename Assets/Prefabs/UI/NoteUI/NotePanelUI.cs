using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotePanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private ScrollbarPrefab _scrollBar;

    private NoteData _noteData;

    public void Init(NoteData noteData)
    {
        _noteData = noteData;
        _title.text = _noteData.Title.GetLocalization(Localizator.main.SelectedLaunguage);
        _text.text = _noteData.Text.GetLocalization(Localizator.main.SelectedLaunguage);
    }
}
