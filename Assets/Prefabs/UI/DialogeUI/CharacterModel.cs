using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]
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
    [SerializeField] private List<Sprite> _emotionIcon;
    [SerializeField] private LocalizationData _name;

    public CharacterType Character => _character;
    public LocalizationData Name => _name;

    public Sprite GetEmotionIcon(CharacterEmotionsType emotion)
    {
        return _emotionIcon[((int)emotion)];
    }
}
