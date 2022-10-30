using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullableObj : MonoBehaviour
{
    public virtual void SetTransform(Vector2 pos, float rot)
    {
        transform.position = pos;
        transform.eulerAngles = new Vector3(0,0, rot);
    }
    public virtual void SetTransform(Vector2 pos)
    {
        transform.position = pos;
    }

    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
