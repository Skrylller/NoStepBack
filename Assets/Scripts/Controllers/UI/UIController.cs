using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController main;

    private void Awake()
    {
        main = this;
    }
}
