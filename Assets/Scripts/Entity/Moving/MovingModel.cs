using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MovingModel
{
    float Speed { get; }
    bool isRun { get; set; }
}

public interface JumpingModel
{
    float JumpForce { get; }
}

public interface SitModel
{
    float CrawlingSpeedMultiplier { get; }
    bool isSit { get; set; }
}

public interface ClimpingModel
{
    float StairClimpingSpeed { get; }
    float StairClimpingHeight { get; }
}
