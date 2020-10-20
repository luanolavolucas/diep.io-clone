using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState_Patrol : AIState
{
    Vector3 patrolTarget;
    public AIState_Patrol(Ship ship, AI ai)
            : base(ship, ai)
    {
        name = State.PATROL; // Set name of current state.
    }

    public override void Enter()
    {
        Debug.Log("Patrolling.");
        SetPatrolTarget();
        base.Enter();
    }

    public override void Update()
    {
        if (ArrivedAtPatrolTarget())
        {
            SetPatrolTarget();
        }
        else
        {
            Vector3 direction = Vector3.Normalize(patrolTarget - ship.transform.position);
            ship.Move(direction.x, direction.y);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    void SetPatrolTarget()
    {
        Debug.Log("Finding a new patrol target.");
        patrolTarget = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        Debug.Log(patrolTarget);
    }

    bool ArrivedAtPatrolTarget()
    {
        return Vector3.Magnitude(ship.transform.position - patrolTarget) < 3.0f;
    }
}
