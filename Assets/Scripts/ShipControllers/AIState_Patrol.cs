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
        //Debug.Log("Patrolling.");
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
            ship.MoveTowards(patrolTarget);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    void SetPatrolTarget()
    {
        Vector3 minPoint = GameManager.Instance.GameArea.Bounds.min;
        Vector3 maxPoint = GameManager.Instance.GameArea.Bounds.max;
        patrolTarget = new Vector2(Random.Range(minPoint.x, maxPoint.x), Random.Range(minPoint.y, maxPoint.y));
    }

    bool ArrivedAtPatrolTarget()
    {
        return Vector3.Magnitude(ship.transform.position - patrolTarget) < 3.0f;
    }




}
