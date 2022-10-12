using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerStairsController : MonoBehaviour
{
    private ClimpingModel _model;

    private BoxCollider2D _collider;
    private Rigidbody2D _rb;
    private List<Collider2D> _collisions = new List<Collider2D>();

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _collisions.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _collisions.Remove(collision);
    }

    public void Init(ClimpingModel model, Rigidbody2D rigidbody2D)
    {
        _model = model;
        _rb = rigidbody2D;
    }

    public void CheckStair(float directional)
    {
        if (_collisions.Count == 0)
            return;

        Collider2D collider = _collisions[0];

        for (int i = 1; i < _collisions.Count; i++)
        {
            if (Mathf.Abs(_collisions[i].bounds.max.x - _collider.bounds.center.x) < Mathf.Abs(collider.bounds.max.x - _collider.bounds.center.x) 
                || Mathf.Abs(_collisions[i].bounds.min.x - _collider.bounds.center.x) < Mathf.Abs(collider.bounds.min.x - _collider.bounds.center.x))
            {
                collider = _collisions[i];
            }
        }

        if (collider.bounds.max.y - transform.position.y <= _model.StairClimpingHeight 
            && (directional > 0 && Mathf.Abs(collider.bounds.min.x - _collider.bounds.center.x) < Mathf.Abs(collider.bounds.max.x - _collider.bounds.center.x)
            || directional < 0 && Mathf.Abs(collider.bounds.min.x - _collider.bounds.center.x) > Mathf.Abs(collider.bounds.max.x - _collider.bounds.center.x)))
        {
                _rb.MovePosition(new Vector2(transform.position.x, collider.bounds.max.y + 0.1f));
        }
    }
}
