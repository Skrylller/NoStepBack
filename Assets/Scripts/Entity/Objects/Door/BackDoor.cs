using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDoor : MonoBehaviour
{
    [SerializeField] private Transform _teleportPoint;
    private Transform _playerTransform;
    private PositionTarget _cameraTarget;

    private void Start()
    {
        _playerTransform = Bootastrap.main.Player.transform;
        _cameraTarget = Bootastrap.main.CameraTarget;
    }

    public void Teleportation()
    {
        _playerTransform.position = _teleportPoint.position;
        _cameraTarget.Teleport(new Vector3(0,50,0));
    }
}
