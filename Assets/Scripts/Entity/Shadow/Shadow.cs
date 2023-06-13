using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] List<ShadowLink> _links;
    [SerializeField] List<AnimationEventController> _animators = new List<AnimationEventController>();

    public bool IsHide => _isHide;
    public int IsObservableDoorOpen;

    private bool _isHide;

    public void UpdateState(bool isHide)
    {
        if (_isHide == isHide)
            return;

        _isHide = isHide;

        if (_isHide)
        {
            IsObservableDoorOpen = 0;
            Hide();

            for (int i = 0; i < _links.Count; i++)
            {
                if(_links[i].Door != null)
                    _links[i].Door.OnChangeState += _links[i].Shadow.ObserveDoor;
            }

            for (int i = 0; i < _links.Count; i++)
            {
                if (_links[i].Door == null || _links[i].Door.State == 1)
                {
                    _links[i].Shadow.IsObservableDoorOpen = 1;
                    _links[i].Shadow.Hide();
                }
                else
                {
                    _links[i].Shadow.IsObservableDoorOpen = 0;
                    _links[i].Shadow.Show();
                }
            }
        }
        else
        {
            for (int i = 0; i < _links.Count; i++)
            {
                if (_links[i].Door != null)
                    _links[i].Door.OnChangeState -= _links[i].Shadow.ObserveDoor;

                if (!_links[i].Shadow.IsHide)
                {
                    _links[i].Shadow.IsObservableDoorOpen = 0;
                    _links[i].Shadow.Show();
                }
            }

            if (IsObservableDoorOpen == 0)
                Show();
        }
    }

    public void Show()
    {
        _animators.ForEach(x => x.SetAnimatorState(0));
    }

    public void Hide()
    {
        _animators.ForEach(x => x.SetAnimatorState(1));
    }

    private void ObserveDoor(int value)
    {
        IsObservableDoorOpen = value;

        if (IsHide)
            return;

        if (value == 0)
            Show();
        else
            Hide();
    }
}

[System.Serializable]
public class ShadowLink
{
    [SerializeField] public Shadow Shadow;
    [SerializeField] public ModeSwitcher Door;
}
