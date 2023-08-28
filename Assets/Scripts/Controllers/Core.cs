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

    public T[] Shuffle<T>(T[] a)
    {
        for (int i = a.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T tmp = a[i];
            a[i] = a[j];
            a[j] = tmp;
        }

        return a;
    }
}
