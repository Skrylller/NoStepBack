using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject _captureButton;
    [SerializeField] private TMP_Text _captureButtonText;

    public void ActiveCapture()
    {
        _captureButton.SetActive(true);
        _captureButtonText.text = PlayerInputSystemPC.main.InputButtons[1].KeyCode.ToString();
    }
    public void DeactiveCapture()
    {
        _captureButton.SetActive(false);
    }
}
