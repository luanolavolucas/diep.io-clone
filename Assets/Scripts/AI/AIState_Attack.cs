using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState_Attack : AIState
{
    Ship target;
    float firingInterval = 3.0f;
    float lastFired = 0.0f;
    public AIState_Attack(Ship ship, AI ai)
                : base(ship, ai)
    {
        name = State.ATTACK;
    }

    public override void Enter()
    {
        target = ai.detectedEnemies[0];
        Debug.Log("Attacking.");
        ship.Brake(3);
        base.Enter();


    }
    public override void Update()
    {
        ship.Aim(target.transform.position);
        if (Time.time - lastFired > firingInterval)
        {
            ship.Fire();
            lastFired = Time.time;
        }

        if (Vector3.Distance(target.transform.position, ship.transform.position) > ai.detectionRadius)
        {
            nextState = new AIState_Chase(ship, ai);
            phase = Phase.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
