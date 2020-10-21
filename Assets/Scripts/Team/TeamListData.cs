using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Team List", menuName = "ScriptableObjects/Team List Data")]
public class TeamListData : ScriptableObject
{
    [SerializeField]
    Team[] teams;
}
