using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSitController : MonoBehaviour
{
    public enum State
    {
        def,
        sit
    }

    [SerializeField] private ModeSwitcher _switcher;

    private SitModel _model;
    private Rigidbody2D _rb;

    private void Update()
    {
        if (_rb.velocity.y != 0)
            Sit(false);
    }

    public void Init(SitModel model, Rigidbody2D rb)
    {
        _model = model;
        _rb = rb;
    }

    public void Sit(bool isSit)
    {
        if (_model.isSit == isSit)
            return;

        _switcher.State = isSit ? (int)State.sit : (int)State.def;
        _model.isSit = isSit;
    }
}
