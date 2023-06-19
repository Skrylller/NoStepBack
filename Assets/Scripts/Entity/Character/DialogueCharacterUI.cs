using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueCharacterUI : MonoBehaviour
{
    [SerializeField] private AnimationEventController _animationEventController;
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _nextPageText;

    private void Start()
    {
        _dialoguePanel.SetActive(false);
    }

    public void ShowDialogue(LocalizationData dialogue, bool lastPage)
    {
        StopCoroutine(CourotineHide());

        _text.text = dialogue.GetLocalization();

        _dialoguePanel.SetActive(true);
        _animationEventController.SetAnimatorState(1);
        _nextPageText.gameObject.SetActive(!lastPage);

        StartCoroutine(CourotineHide());
    }

    private IEnumerator CourotineHide()
    {
        yield return new WaitForSeconds(10f);

        _animationEventController.SetAnimatorState(0);
    }
}
