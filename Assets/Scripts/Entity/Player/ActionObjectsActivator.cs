using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionObjectsActivator : MonoBehaviour
{
    private List<ActionObject> _actionObjects = new List<ActionObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActionObject actionObject = collision.GetComponent<ActionObject>();

        if (actionObject != null)
        {
            if (_actionObjects.Count > 0)
                _actionObjects[_actionObjects.Count - 1].Exit();

            _actionObjects.Add(actionObject);

            _actionObjects[_actionObjects.Count - 1].Enter();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ActionObject actionObject = collision.GetComponent<ActionObject>();

        if (_actionObjects.Count > 0)
        {
            if (_actionObjects[_actionObjects.Count - 1] == actionObject)
                _actionObjects[_actionObjects.Count - 1].Exit();

            _actionObjects.Remove(actionObject);

            if (_actionObjects.Count > 0)
                _actionObjects[_actionObjects.Count - 1].Enter();
        }
    }

    public void Action()
    {
        if (Bootastrap.main.Player.stopInput)
            return;

        if(_actionObjects.Count > 0)
            _actionObjects[_actionObjects.Count - 1].Action();
    }
}
