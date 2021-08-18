using UnityEngine;

[CreateAssetMenu(fileName = "New Match Setup Data", menuName = "Game Design/Match Setup Data", order = 1)]
public class MatchSetupData : ScriptableObject
{
    public int maxShips = 10;
    public int maxPowerUps = 5;
    public float timeBetweenAISpawns;
    public float timeBetweenCrateSpawns;

}
