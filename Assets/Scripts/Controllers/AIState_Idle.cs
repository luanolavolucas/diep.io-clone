using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState_Idle : AIState
{

    public AIState_Idle(Character ship, AI ai)
                : base(ship, ai)
    {
        name = State.IDLE; 
    }

    public override void Enter()
    {
        Debug.Log("Idling.");
        base.Enter();
    }
    public override void Update()
    {
        if (Random.Range(0, 100) < 10)
        {
            nextState = new AIState_Patrol(ship, ai);
            phase = Phase.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
