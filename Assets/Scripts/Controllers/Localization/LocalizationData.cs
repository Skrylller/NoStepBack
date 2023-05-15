using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "LOCAL_NAME", menuName = "ScriptableObjects/Localization")]
public class LocalizationData : ScriptableObject
{
    [SerializeField] private List<LaunguageData> data = new List<LaunguageData>();

    public string GetLocalization(Localizator.Launguage launguage)
    {
        return data.Where(x => x.launguage == launguage).ToList().First().text;
    }
}

[System.Serializable]
public class LaunguageData
{
    public Localizator.Launguage launguage;

    [TextArea(10,100)]
    public string text;
}
