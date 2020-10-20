using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState_Idle : AIState
{

    public AIState_Idle(Ship ship, AI ai)
                : base(ship, ai)
    {
        name = State.IDLE; // Set name of current state.
    }

    public override void Enter()
    {
        base.Enter(); 
    }
    public override void Update()
    {
        // The only place where Update can break out of itself. Set chance of breaking out at 10%.
        if (Random.Range(0, 100) < 10)
        {
            nextState = new AIState_Patrol(ship, ai);
            phase = Phase.EXIT; // The next time 'Process' runs, the EXIT phase will run instead, which will then return the nextState.
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
