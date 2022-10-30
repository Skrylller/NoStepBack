using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovingModel
{
    float Speed { get; }
    bool CanRun { get; }
    bool isRun { get; set; }
}

public interface IJumpingModel
{
    float JumpForce { get; }
}

public interface ISitModel
{
    float CrawlingSpeedMultiplier { get; }
    bool isSit { get; set; }
}

public interface IClimpingModel
{
    float StairClimpingSpeed { get; }
    float StairClimpingHeight { get; }
}
