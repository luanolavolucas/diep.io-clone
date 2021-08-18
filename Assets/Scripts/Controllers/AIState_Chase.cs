using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState_Chase : AIState
{
    Character target;
    public AIState_Chase(Character character, AI ai)
                : base(character, ai)
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
            nextState = new AIState_Idle(character, ai);
            phase = Phase.EXIT;
            return;
        }

        character.Aim(target.transform.position);
        if(Vector3.Distance(target.transform.position,character.transform.position) > ai.shootingRadius)
        {
            character.MoveTowards(target.transform.position);
        }
        else
        {
            nextState = new AIState_Attack(character, ai);
            phase = Phase.EXIT;
        }
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
