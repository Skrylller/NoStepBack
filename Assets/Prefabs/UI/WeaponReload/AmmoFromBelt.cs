using System;
using UnityEngine;

public class AmmoFromBelt : MonoBehaviour
{
    [SerializeField] private DragObject _dragObject;
    [SerializeField] private ModeSwitcher _modeSwitcher;

    private Transform _dropPosition;
    private ItemModel.ItemType _ammoType;

    public Action OnReload;
    public Func<bool> OnAmmoCheck;

    private void Start()
    {
        _dragObject.OnUp += MouseUp;
    }

    public void Init(ItemModel.ItemType ammoType, int state, Transform dropPos)
    {
        _dropPosition = dropPos;
        _ammoType = ammoType;
        _modeSwitcher.State = state;
        _dragObject.Init();
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

        _modeSwitcher.State = (int)WeaponReloadUI.AmmoState.havent;
        _dragObject.transform.localPosition = Vector3.zero;

        InventoryItem inventoryItem = PlayerInventory.Inventory.GetInventoryItem(_ammoType);

        if(inventoryItem != null)
            PlayerInventory.main.DropItem(inventoryItem.ItemModel, _dropPosition);
    }
}
