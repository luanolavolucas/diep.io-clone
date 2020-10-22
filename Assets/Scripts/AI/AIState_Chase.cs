using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState_Chase : AIState
{
    Ship target;
    public AIState_Chase(Ship ship, AI ai)
                : base(ship, ai)
    {
        name = State.CHASE;
    }

    public override void Enter()
    {
        target = ai.detectedEnemies[0];
        base.Enter();
    }
    public override void Update()
    {
        if (target == null)
        {
            nextState = new AIState_Idle(ship, ai);
            phase = Phase.EXIT;
            return;
        }

        ship.Aim(target.transform.position);
        if(Vector3.Distance(target.transform.position,ship.transform.position) > ai.shootingRadius)
        {
            ship.MoveTowards(target.transform.position);
        }
        else
        {
            nextState = new AIState_Attack(ship, ai);
            phase = Phase.EXIT;
        }
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
