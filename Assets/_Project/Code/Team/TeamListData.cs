using UnityEngine;

[CreateAssetMenu(fileName = "New Team List", menuName = "Game Design/Team List")]
public class TeamListData : ScriptableObject
{
    [SerializeField]
    private Team[] teams;
}
