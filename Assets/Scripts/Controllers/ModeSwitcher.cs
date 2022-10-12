using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    [System.Serializable]
    private class Mode
    {
        [SerializeField] private string name;

        [SerializeField] private List<GameObject> objs;
        [SerializeField] private List<TransformMode> transforms;

        public void ActivateMode()
        {
            objs?.ForEach(x => x.SetActive(true));
            transforms?.ForEach(x => x.SetTransform());
        }

        public void DeactivateMode()
        {
            objs?.ForEach(x => x.SetActive(false));
        }

        [System.Serializable]
        private class TransformMode
        {
            [SerializeField] private Transform obj;
            [SerializeField] private Vector3 position;
            [SerializeField] private Vector3 rotation;
            [SerializeField] private Vector3 scale = new Vector3(1,1,1);

            public void SetTransform()
            {
                obj.localPosition = position;
                obj.localEulerAngles = rotation;
                obj.localScale = scale;
            }
        }
    }

    [SerializeField] private int _state;

    [SerializeField] private List<Mode> _states;
    
    public int State { 
        get 
        {
            return _state;
        }
        set 
        {
            if (_states.Count <= value)
                value = 0;

            _state = value;
            SetState(value);
        } 
    }

    private void Awake()
    {
        SetState(_state);
    }

    private void SetState(int value)
    {
        _states.ForEach(x => x.DeactivateMode());
        _states[value].ActivateMode();
    }
}
