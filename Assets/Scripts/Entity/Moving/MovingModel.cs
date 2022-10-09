using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MovingModel
{
    float Speed { get; }
}

public interface JumpingModel
{
    float JumpForce { get; }
}

public interface ClimpingModel
{
    float StairClimpingSpeed { get; }
}
