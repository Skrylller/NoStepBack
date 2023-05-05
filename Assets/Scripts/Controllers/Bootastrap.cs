using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootastrap : MonoBehaviour
{
    public static Bootastrap main;

    [SerializeField] private PlayerController _player;

    public PlayerController Player => _player;

    private void Awake()
    {
        main = this;
    }
}
