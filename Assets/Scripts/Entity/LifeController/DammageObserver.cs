using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DammageObserver : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LifeController _lifeController;
    [Range(0,1)]
    [SerializeField] private float _dammageFactor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != _layerMask) 
            return;

        DammageArea dammageArea = collision.gameObject.GetComponent<DammageArea>();

        if (dammageArea != null)
        {
            TakeDammage(dammageArea.Dammage);
        }
    }

    public void TakeDammage(uint dammage)
    {
        _lifeController.TakeDammage((uint)Mathf.Abs(dammage * _dammageFactor));
    }
}
