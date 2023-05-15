using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
[CreateAssetMenu(fileName = "NAME_LOCAL", menuName = "ScriptableObjects/Localization")]
public class LocalizationData : ScriptableObject
{
    [SerializeField] private List<LaunguageData> data = new List<LaunguageData>();

    public string GetLocalization(Localizator.Launguage launguage)
    {
        List<LaunguageData> needData = data.Where(x => x.launguage == launguage).ToList();

        if (needData.Count <= 0)
            return data.First().text;
        else
            return needData.First().text;
    }
}

[System.Serializable]
public class LaunguageData
{
    public Localizator.Launguage launguage;

    [TextArea(10,100)]
    public string text;
}
