using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerObject : MonoBehaviour
{
    [SerializeField] private bool _isEnterDeactivate;

    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private UnityEvent OnStay;
    [SerializeField] private UnityEvent OnExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke();

        if (_isEnterDeactivate)
            gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnStay?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnExit?.Invoke();
    }
}
