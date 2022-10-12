using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : MonoBehaviour
{
    [SerializeField] private ModeSwitcher _state;

    public void SwitchState()
    {
        _state.State++;
    }
}
