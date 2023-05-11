using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarPrefab : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private RectTransform _scrollableObject;
    [SerializeField] private RectTransform _scrollableObjectMask;

    private float _startPosition;
    private float _movePosition;

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        if (_scrollableObjectMask.rect.height > _scrollableObject.rect.height)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        _startPosition = _scrollableObject.anchoredPosition.y;
        _scrollbar.value = 0;
        _scrollbar.size = _scrollableObjectMask.rect.height / _scrollableObject.rect.height;
    }

    public void Scroll()
    {
        _movePosition = _startPosition + (_scrollableObject.rect.height - _scrollableObjectMask.rect.height) * _scrollbar.value;
        _scrollableObject.anchoredPosition = new Vector2(_scrollableObject.anchoredPosition.x, _movePosition);
    } 
}
