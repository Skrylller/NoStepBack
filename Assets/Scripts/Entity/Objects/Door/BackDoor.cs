using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDoor : MonoBehaviour
{
    [SerializeField] private Transform _teleportPoint;
    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = Bootastrap.main.Player.transform;
    }

    public void Teleportation()
    {
        _playerTransform.position = _teleportPoint.position;
    }
}
