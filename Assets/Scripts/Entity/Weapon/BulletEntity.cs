using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletEntity : PullableObj
{
    [SerializeField] private PullableObj _particle;
    [SerializeField] private LayerMask _layerMask;

    private PullObjects _particlesPull;
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
        if (_model.RaycastPhysics)
            CheckPosHit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_model.RaycastPhysics)
            return;

        if (collision.gameObject.layer != 14)
            Deactivate();
    }

    public void Init(BulletModel model, Transform defPos, float angle = 0)
    {
        _model = model;
        _particlesPull = PullsController.main.GetPull(_particle);

        transform.position = defPos.position;

        transform.eulerAngles += new Vector3(0, 0, angle);
        _direction = new Vector2(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270 ? -1 : 1, transform.eulerAngles.z > 180 ? -1 : 1);

        if (_model.RaycastPhysics)
        {
            _hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.left), _model.DistanceBackCheck, _layerMask);
            if (_hit.collider != null)
            {
                DestroyBullet();
                return;
            }


            _hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right), _model.Distance, _layerMask);

            //Debug.Log($"{_hit.point} {_direction} {transform.eulerAngles.z}");

            StartCoroutine(DeactivateCourotine());
        }

        Vector3 direction = new Vector3(_model.Speed * Mathf.Cos(transform.localEulerAngles.z * Mathf.Deg2Rad), _model.Speed * Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad), 0);
        _rigidbody2D.velocity = direction;
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
        DammageObserver dammageObserver = _hit.collider.gameObject.GetComponent<DammageObserver>();

        if (dammageObserver != null)
            dammageObserver.TakeDammage(_model.Dammage, _hit.point, transform.eulerAngles.z);

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
