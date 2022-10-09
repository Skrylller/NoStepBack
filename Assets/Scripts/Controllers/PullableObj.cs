using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullableObj : MonoBehaviour
{
    public virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
