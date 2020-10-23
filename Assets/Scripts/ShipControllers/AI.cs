using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship))]
public class AI : MonoBehaviour
{
    Ship ship;
    public float detectionRadius = 12.0f;
    public float shootingRadius = 8.0f;
    public float avoidRadius = 4.0f;
    public  List<Ship> detectedAllies;
    public List<Ship> detectedEnemies;
    [SerializeField]
    AIState currentState;
    void Start()
    {
        ship = GetComponent<Ship>();
        detectedAllies = new List<Ship>();
        detectedEnemies = new List<Ship>();
        currentState = new AIState_Idle(ship, this);
    }

    void Update()
    {
        currentState = currentState.Process(); 
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
