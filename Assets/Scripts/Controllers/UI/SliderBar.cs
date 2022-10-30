using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBar : MonoBehaviour
{
    [SerializeField] private Image _sliderLine;
    [SerializeField] private GameObject _sliderVisitorObject;

    private void Awake()
    {
        ISliderVisitor _sliderVisitor = _sliderVisitorObject.GetComponent<ISliderVisitor>();

        if (_sliderVisitor != null)
            _sliderVisitor.OnChange += SetSlider;
        else
            Debug.Log($"Dont have slider visitor");

    }

    private void SetSlider(float value)
    {
        _sliderLine.fillAmount = value;
    }
}

public interface ISliderVisitor
{
    Action<float> OnChange { get; set; }
    float sliderValue { set; }
}
