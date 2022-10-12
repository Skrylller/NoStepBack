using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSitController : MonoBehaviour
{
    public bool sit { get; private set; }

    public void Sit(bool isSit)
    {
        if (sit == isSit)
            return;

        sit = isSit;
    }
}
