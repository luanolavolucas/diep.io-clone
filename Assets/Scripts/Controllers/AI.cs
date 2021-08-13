using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class AI : CharacterControl
{
    public float detectionRadius = 12.0f;
    public float shootingRadius = 8.0f;
    public float avoidRadius = 4.0f;
    public  List<Character> detectedAllies;
    public List<Character> detectedEnemies;
    [SerializeField]
    AIState currentState;

    override protected void Awake()
    {
        base.Awake();
        detectedAllies = new List<Character>();
        detectedEnemies = new List<Character>();
        currentState = new AIState_Idle(character, this);
        if (!photonView.IsMine)
            this.enabled = false;
    }
    void Start()
    {
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
