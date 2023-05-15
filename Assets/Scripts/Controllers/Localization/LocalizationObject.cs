using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizationObject : MonoBehaviour
{
    [SerializeField] private LocalizationData _localizationData;

    [SerializeField] private Text _text;
    [SerializeField] private TextMeshProUGUI _tmp;

    public void Start()
    {
        Localizator.main.OnChangeLaunguage += Localize;
        Localize(Localizator.main.SelectedLaunguage);
    }

    public void OnDestroy()
    {
        Localizator.main.OnChangeLaunguage -= Localize;
    }

    private void Localize(Localizator.Launguage launguage)
    {
        string text = _localizationData.GetLocalization(launguage);

        if(_text != null)
            _text.text = text;

        if (_tmp != null)
            _tmp.text = text;
    }
}
