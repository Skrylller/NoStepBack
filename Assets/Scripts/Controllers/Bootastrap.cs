using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootastrap : MonoBehaviour
{
    public static Bootastrap main;

    [SerializeField] private PlayerController _player;
    [SerializeField] private PositionTarget _cameraTarget;

    public PlayerController Player => _player;
    public PositionTarget CameraTarget => _cameraTarget;

    private void Awake()
    {
        main = this;
    }
}
