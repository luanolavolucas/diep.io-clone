using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreCollector
{
    float Score { get; }
    void AddToScore(float value);
    void ResetScore();
}
