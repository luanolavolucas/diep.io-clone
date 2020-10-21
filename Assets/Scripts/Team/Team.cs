using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Team", menuName = "ScriptableObjects/Team Data")]
public class Team: ScriptableObject
{
    public string teamName;
    public Color teamColor = Color.white;
}
