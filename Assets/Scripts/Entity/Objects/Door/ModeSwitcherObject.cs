using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcherObject : MonoBehaviour
{
    [SerializeField] private ModeSwitcher _state;

    public void SwitchState()
    {
        _state.State++;
    }
}
