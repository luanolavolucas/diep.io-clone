using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship))]
public class AI : MonoBehaviour
{
    Ship ship;
    public float detectionRadius = 10.0f;
    AIState currentState;
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Ship>();
        currentState = new AIState_Idle(ship, this); // Create our first state.
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process(); // Calls Process method to ensure correct state is set.
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
