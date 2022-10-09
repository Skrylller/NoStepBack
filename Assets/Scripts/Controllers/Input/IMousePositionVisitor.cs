using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMousePositionVisitor
{
    void CheckMouse(Vector2 mousePos);
}
