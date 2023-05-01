using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSlot : MonoBehaviour
{
    [SerializeField] private ModeSwitcher _modeSwitcher;
    public void SetState(int value)
    {
        _modeSwitcher.State = value;
    }
}
