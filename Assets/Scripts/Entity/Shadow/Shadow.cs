using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] List<ShadowLink> _links;
    [SerializeField] AnimationEventController _animator;

    public bool IsHide => _isHide;

    private bool _isHide;

    public void UpdateState(bool isHide)
    {
        _isHide = isHide;

        if (_isHide)
        {
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
                    _links[i].Shadow.Hide();
                }
                else
                {
                    _links[i].Shadow.Show();
                }
            }
        }
        else
        {
            for (int i = 0; i < _links.Count; i++)
            {
                _links[i].Door.OnChangeState -= _links[i].Shadow.ObserveDoor;

                if (!_links[i].Shadow.IsHide)
                    _links[i].Shadow.Show();
            }
        }
    }

    public void Show()
    {
        _animator.SetAnimatorState(0);
    }

    public void Hide()
    {
        _animator.SetAnimatorState(1);
    }

    private void ObserveDoor(int value)
    {
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