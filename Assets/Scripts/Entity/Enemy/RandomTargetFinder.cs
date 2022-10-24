using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTargetFinder : MonoBehaviour
{
    [SerializeField] private Transform _pointUp;
    [SerializeField] private Transform _pointDown;

    [SerializeField] private LayerMask _layerMask;

    [HideInInspector] public Vector2 target;

    public void FindNewTarget()
    {
        RaycastHit2D hitDown;
        RaycastHit2D hitUp;
        Vector2 distance;

        hitDown = Physics2D.Raycast(_pointDown.position, Vector2.right, 10000, _layerMask);
        hitUp = Physics2D.Raycast(_pointUp.position, Vector2.right, 10000, _layerMask);

        distance = new Vector2(0, hitDown.point.x < hitUp.point.x ? hitDown.point.x : hitUp.point.x);

        hitDown = Physics2D.Raycast(_pointDown.position, Vector2.left, 10000, _layerMask);
        hitUp = Physics2D.Raycast(_pointUp.position, Vector2.left, 10000, _layerMask);

        distance = new Vector2(hitDown.point.x > hitUp.point.x ? hitDown.point.x : hitUp.point.x, distance.y);

        target = new Vector2(Random.Range(distance.x, distance.y), transform.position.y);
    }
}
