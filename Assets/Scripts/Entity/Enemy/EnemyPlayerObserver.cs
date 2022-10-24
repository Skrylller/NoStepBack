using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyPlayerObserver : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private IPlayerObsrver _model;
    private Collider2D _player;
    private bool isFind;

    private const string _playerLayer = "Player";

    public Action<GameObject> OnView;
    public bool isView;

    private void OnEnable()
    {
        isFind = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(_playerLayer) && collision.gameObject.GetComponent<PlayerController>())
        {
            _player = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(_playerLayer))
        {
            if (_player == collision)
                _player = null;
        }
    }

    private void Update()
    {
        ViewPlayer();
    }

    public void Init(IPlayerObsrver model)
    {
        _model = model;
    }

    public IEnumerator StopFind(float time)
    {
        isFind = false;
        yield return new WaitForSeconds(time);
        isFind = true;
    }

    private void ViewPlayer()
    {

        if (_player == null || !isFind)
        {
            isView = false;
            return;
        }

        if (_model.IsViewAlways)
        {
            View();
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _player.bounds.center - transform.position, _model.ViewDistance, _layerMask);
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer(_playerLayer))
        {
            View();
            return;
        }

        isView = false;
    }

    private void View()
    {
        OnView?.Invoke(_player.gameObject);
        isView = true;
    }
}
