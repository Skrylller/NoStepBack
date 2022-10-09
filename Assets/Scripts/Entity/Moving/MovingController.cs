using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour
{
    private MovingModel _model;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        if (GetComponentInParent<Rigidbody2D>())
            _rigidbody = GetComponentInParent<Rigidbody2D>();
        else
            Debug.Log($"{gameObject.name} Отсутствует {nameof(Rigidbody2D)}");
    }

    public void Init(MovingModel model)
    {
        _model = model;
    }

    public void MoveHorizontal(float directional)
    {
        _rigidbody.velocity = new Vector2(_model.Speed * directional, _rigidbody.velocity.y);
    }
}
