using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour
{
    private IMovingModel _model;
    private Rigidbody2D _rigidbody;
    private JumpingController _jumpingController;

    public void Init(IMovingModel model, Rigidbody2D rigidbody2D)
    {
        Init(model, rigidbody2D, null);
    }
    public void Init(IMovingModel model, Rigidbody2D rigidbody2D, JumpingController jumpingController)
    {
        _model = model;
        _rigidbody = rigidbody2D;
        _jumpingController = jumpingController;
    }

    private void FixedUpdate()
    {
        SlowDown();
    }

    public void MoveHorizontal(float directional)
    {
        _rigidbody.velocity = new Vector2(_model.Speed * directional, _rigidbody.velocity.y);
    }

    private void SlowDown()
    {
        if (_jumpingController != null && _jumpingController.IsGrounded > 0)
        {
            float x = Mathf.Abs(_rigidbody.velocity.x) - _model.SlowDownSpeed;

            if (x < 0)
                x = 0;
            else
                x = x * (Mathf.Abs(_rigidbody.velocity.x) / _rigidbody.velocity.x);

            _rigidbody.velocity = new Vector2(x, _rigidbody.velocity.y);
        }
    }
}
