using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour
{
    private IMovingModel _model;
    private Rigidbody2D _rigidbody;

    public void Init(IMovingModel model, Rigidbody2D rigidbody2D)
    {
        _model = model;
        _rigidbody = rigidbody2D;
    }

    public void MoveHorizontal(float directional)
    {
        _rigidbody.velocity = new Vector2(_model.Speed * directional * Time.deltaTime, _rigidbody.velocity.y);
    }
}
