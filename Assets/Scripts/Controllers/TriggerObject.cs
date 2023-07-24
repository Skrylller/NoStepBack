using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerObject : MonoBehaviour
{
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private UnityEvent OnStay;
    [SerializeField] private UnityEvent OnExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke();
        //Debug.Log($"{gameObject.name} Enter");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnStay?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnExit?.Invoke();
        //Debug.Log($"{gameObject.name} Exit");
    }
}
