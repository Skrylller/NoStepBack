using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ActionObjectsActivator : MonoBehaviour
{
    private ActionObject _actionObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionObject actionObject = collision.GetComponent<ActionObject>();

        if (actionObject != null)
        {
            if(_actionObject != null)
                _actionObject.Exit();

            _actionObject = actionObject;
            _actionObject.Enter();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ActionObject actionObject = collision.GetComponent<ActionObject>();

        if (actionObject == _actionObject)
        {
            _actionObject.Exit();
            _actionObject = null;
        }
    }

    public void Action()
    {
        _actionObject.Action();
    }
}