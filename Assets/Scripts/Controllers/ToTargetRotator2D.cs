using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTargetRotator2D : MonoBehaviour, IMousePositionVisitor
{
    [SerializeField] private float _rotationBias;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _useAimObj;
    [SerializeField] private Transform _aimObj;

    private Transform rotatableObj;
    private Vector3 rotate;

    private void Awake()
    {
        rotatableObj = transform;
    }

    private void Update()
    {
        if (_useAimObj)
            Rotate(_aimObj.localPosition);
    }

    public void CheckMouse(Vector2 mousePos)
    {
        Rotate(mousePos);
    }

    public void Rotate(Vector2 aim)
    {
        float angle = Mathf.Atan((rotatableObj.position.y - aim.y) / (rotatableObj.position.x - aim.x)) * Mathf.Rad2Deg;

        if (rotatableObj.position.x - aim.x > 0)
            angle -= 180;

        angle += _rotationBias;

        rotatableObj.eulerAngles = new Vector3(
            rotatableObj.eulerAngles.x, 
            rotatableObj.eulerAngles.y,
            angle
            );
    }
}
