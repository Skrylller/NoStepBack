using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotePanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private ScrollbarPrefab _scrollBar;

    private NoteModel _noteData;

    private void OnEnable()
    {
        if (_noteData != null)
            Localizator.main.OnChangeLaunguage += UpdateLocal;
    }

    private void OnDisable()
    {
        if (_noteData != null)
            Localizator.main.OnChangeLaunguage -= UpdateLocal;
    }

    public void Init(NoteModel noteData)
    {
        if(_noteData == null)
            Localizator.main.OnChangeLaunguage += UpdateLocal;

        _noteData = noteData;
        UpdateWindow();
    }

    private void UpdateLocal(Localizator.Launguage launguage)
    {
        UpdateWindow();
    }

    private void UpdateWindow()
    {
        _title.text = _noteData.Name;
        _text.text = _noteData.Text;
    }
}
