using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ship Data", menuName = "ScriptableObjects/Ship Data", order = 1)]
public class ShipData : ScriptableObject
{
    public float health;
    public float speed;
    public int scoreAwardedWhenDestroyed = 20;
}
