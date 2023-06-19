using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Core : MonoBehaviour
{
    public static Core main;

    public void Awake()
    {
        main = this;
    }



    public IEnumerator CourotineTimer(float timer, Action pastAction, Action presentAction = null)
    {
        presentAction?.Invoke();

        yield return new WaitForSeconds(timer);

        pastAction?.Invoke();
    }
}
