using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionObjectsActivator : MonoBehaviour
{
    private ActionObject _actionObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionObject actionObject = collision.GetComponent<ActionObject>();

        if (actionObject != null)
        {
            _actionObject?.Exit();

            _actionObject = actionObject;
            _actionObject.Enter();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ActionObject actionObject = collision.GetComponent<ActionObject>();

        if (_actionObject != null && actionObject == _actionObject)
        {
            _actionObject.Exit();
            _actionObject = null;
        }
    }

    public void Action()
    { 
        _actionObject?.Action();
    }
}
