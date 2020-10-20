using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageable
{
    float Health { get;}
    void Damage(float dmg);
}
