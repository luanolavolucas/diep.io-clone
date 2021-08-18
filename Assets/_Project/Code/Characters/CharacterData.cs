using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Character Data", menuName = "Game Design/Character Data", order = 1)]
public class CharacterData : ScriptableObject
{
    public float health;
    public float speed;
    public int scoreAwardedWhenDestroyed = 20;
}
