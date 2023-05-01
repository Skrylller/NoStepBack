using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AmmoFromBelt : MonoBehaviour
{
    [SerializeField] private DragObject _dragObject;
    [SerializeField] private ModeSwitcher _modeSwitcher;

    public Action OnReload;
    public Func<bool> OnAmmoCheck;

    private void Start()
    {
        _dragObject.OnUp += MouseUp;
    }

    public void SetState(int value)
    {
        _modeSwitcher.State = value;
    }

    public void MouseUp()
    {
        if (!_dragObject.TakeObj)
            return;


        for (int i = 0; i < _dragObject.Colliders.Count; i++)
        {
            AmmoSlot ammoSlot = _dragObject.Colliders[i].GetComponentInParent<AmmoSlot>();
            if (ammoSlot != null)
            {

                if (!OnAmmoCheck.Invoke())
                {
                    _dragObject.transform.localPosition = Vector3.zero;
                    return;
                }

                ammoSlot.SetState((int)WeaponReloadUI.AmmoState.have);
                _modeSwitcher.State = (int)WeaponReloadUI.AmmoState.havent;
                OnReload?.Invoke();
                _dragObject.transform.localPosition = Vector3.zero;
                return;
            }
        }

        _dragObject.transform.localPosition = Vector3.zero;
        //drop
    }
}
