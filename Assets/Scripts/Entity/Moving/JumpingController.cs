using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class JumpingController : MonoBehaviour
{
    private JumpingModel _model;
    private Rigidbody2D _rigidbody;
    public int IsGrounded { get; private set; }

    public void Init(JumpingModel model, Rigidbody2D rigidbody2D)
    {
        _model = model;
        _rigidbody = rigidbody2D;
    }

    public void Jump()
    {
        _rigidbody.AddForce(new Vector2(0, _model.JumpForce));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsGrounded++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounded--;
    }
}
