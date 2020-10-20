using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Linq;
public class AIState_Patrol : AIState
{
    Vector3 patrolTarget;
    public AIState_Patrol(Ship ship, AI ai)
            : base(ship, ai)
    {
        name = State.PATROL; 
    }

    public override void Enter()
    {
        Debug.Log("Patrolling.");
        SetPatrolTarget();
        base.Enter();
    }

    public override void Update()
    {
        DetectShips();

        if(ai.detectedEnemies.Count > 0)
        {
            nextState = new AIState_Chase(ship, ai);
            phase = Phase.EXIT;
        }

        if (ArrivedAtPatrolTarget())
        {
                nextState = new AIState_Idle(ship, ai);
                phase = Phase.EXIT;
        }
        else
        {
            //Vector3 direction = Vector3.Normalize(patrolTarget - ship.transform.position);
            //ship.Move(direction.x, direction.y);
            ship.MoveTowards(patrolTarget);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    void SetPatrolTarget()
    {
        Debug.Log("Finding a new patrol target.");
        patrolTarget = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20));
    }

    bool ArrivedAtPatrolTarget()
    {
        return Vector3.Magnitude(ship.transform.position - patrolTarget) < 3.0f;
    }




}
