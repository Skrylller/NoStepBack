using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : ScriptableObject
{
    public enum CharacterEmotionsType
    {
        normal,
        happy,
        angry,
        sad,
    }

    public enum CharacterType
    {
        player,
        stranger,
    }

    [SerializeField] private CharacterType _character;
    [SerializeField] private Sprite _icon;
    [SerializeField] private LocalizationData _name;

    public CharacterType Character => _character;
    public Sprite Icon => _icon;
    public LocalizationData Name => _name;
}
