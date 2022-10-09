using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class JumpingController : MonoBehaviour
{
    private JumpingModel _model;
    private Rigidbody2D _rigidbody;
    public int IsGrounded { get; private set; }

    private void Awake()
    {
        if (GetComponentInParent<Rigidbody2D>())
            _rigidbody = GetComponentInParent<Rigidbody2D>();
        else
            Debug.Log($"{gameObject.name} Отсутствует {nameof(Rigidbody2D)}");
    }

    public void Init(JumpingModel model)
    {
        _model = model;
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
