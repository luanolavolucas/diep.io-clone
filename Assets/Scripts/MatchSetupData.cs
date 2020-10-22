using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Match Setup Data", menuName = "ScriptableObjects/Match Setup Data", order = 1)]
public class MatchSetupData : ScriptableObject
{
    public int maxShips = 10;
    public float timeBetweenSpawns;

}
