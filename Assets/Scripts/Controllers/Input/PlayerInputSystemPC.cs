using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputSystemPC : MonoBehaviour
{
    public static PlayerInputSystemPC main;

    [Serializable]
    public class InputButton
    {
        [SerializeField] private string name;
        [SerializeField] private KeyCode keyCode;

        [SerializeField] private UnityEvent actionDown;
        [SerializeField] private UnityEvent actionUp;
        [SerializeField] private UnityEvent actionStay;

        public string Name { get { return name; } }
        public KeyCode KeyCode { get { return keyCode; } }

        public void CheckTouch()
        {
            if (Input.GetKeyDown(keyCode)) 
                actionDown?.Invoke();

            if (Input.GetKeyUp(keyCode)) 
                actionUp?.Invoke();

            if (Input.GetKey(keyCode)) 
                actionStay?.Invoke();
        }
    }

    [SerializeField] private List<InputButton> _inputButtons;

    [Header("Mouse position input")]
    [SerializeField] private List<GameObject> _mousePositionVisitors;

    private Action<Vector2> _actionMouse;
    public List<InputButton> InputButtons { get { return _inputButtons; } }

    private void Awake()
    {
        main = this;

        foreach (GameObject objs in _mousePositionVisitors)
        {
            if(objs.GetComponent<IMousePositionVisitor>() != null) 
                _actionMouse += objs.GetComponent<IMousePositionVisitor>().CheckMouse;
        }
    }

    private void OnDestroy()
    {
        _actionMouse -= _actionMouse;
    }

    private void Update()
    {
        _inputButtons.ForEach(x => x.CheckTouch());

        MousePosition();
    }

    public void MousePosition()
    {
        _actionMouse?.Invoke(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
