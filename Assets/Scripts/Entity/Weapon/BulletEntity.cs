using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletEntity : PullableObj
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private PullObjects _particlesPull;

    private BulletModel _model;
    private Rigidbody2D _rigidbody2D;
    private RaycastHit2D _hit;
    private Vector2 _direction;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckPosHit();
    }

    public void Init(BulletModel model, PullObjects pull, float angle = 0)
    {
        _model = model;
        _particlesPull = pull;

        transform.eulerAngles += new Vector3(0, 0, angle);
        _direction = new Vector2(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270 ? -1 : 1, transform.eulerAngles.z > 180 ? -1 : 1);

        _hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.left), _model.DistanceBackCheck, _layerMask);
        if(_hit.collider != null)
        {
            DestroyBullet();
            return;
        }


        _hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right), _model.Distance, _layerMask);
        //Debug.Log($"{_hit.point} {_direction} {transform.eulerAngles.z}");

        Vector3 direction = new Vector3(_model.Speed * Mathf.Cos(transform.localEulerAngles.z * Mathf.Deg2Rad), _model.Speed * Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad), 0);
        _rigidbody2D.velocity = direction;
        StartCoroutine(DeactivateCourotine());
    }

    private void CheckPosHit()
    {
        if (_hit.collider != null && (0 <= (transform.position.x - _hit.point.x) * _direction.x || 0 <= (transform.position.y - _hit.point.y) * _direction.y))
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        PullableObj part = _particlesPull.AddObj();
        part.transform.position = _hit.point;
        Deactivate();
    }

    private IEnumerator DeactivateCourotine()
    {
        yield return new WaitForSeconds(_model.LifeTime);
        Deactivate();
    }
}
