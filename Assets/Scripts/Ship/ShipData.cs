using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ship Data", menuName = "ScriptableObjects/Ship Data", order = 1)]
public class ShipData : ScriptableObject
{
    public float health;
    public float speed;
    public Team team;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum Team { Red,Blue}
