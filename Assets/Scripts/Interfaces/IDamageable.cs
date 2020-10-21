using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    float Health { get;}
    void Damage(float dmg, IScoreCollector responsible = null);
}
