using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Localizator : MonoBehaviour
{
    public static Localizator main;

    public enum Launguage
    {
        English,
        Russia
    }

    [SerializeField] private Launguage _selectedLaunguage;

    public Launguage SelectedLaunguage => _selectedLaunguage;

    public Action<Launguage> OnChangeLaunguage;

    public void Awake()
    {
        main = this;
    }

    public void ChangeLaunguage(Launguage launguage)
    {
        if (_selectedLaunguage == launguage)
            return;

        _selectedLaunguage = launguage;
        OnChangeLaunguage?.Invoke(_selectedLaunguage);
    }
}
