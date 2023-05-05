using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DragObject : MonoBehaviour
{
    private bool takeObj = false;

    public bool TakeObj => takeObj;

    public List<Collider2D> Colliders { get; private set; } = new List<Collider2D>();
    public Action OnDown;
    public Action OnDrag;
    public Action OnUp;

    private void OnMouseDown()
    {
        takeObj = true;
        OnDown?.Invoke();
    }

    private void OnMouseDrag()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(position.x, position.y, 0);
        OnDrag?.Invoke();
    }

    private void OnMouseUp()
    {
        if (takeObj)
        {
            OnUp?.Invoke();
            takeObj = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Colliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Colliders.Remove(collision);
    }

    public void Init()
    {
        takeObj = false;
    }
}
