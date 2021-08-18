using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class AIState_Attack : AIState
{
    Character target;
    float firingInterval = 3.0f;
    float lastFired = 0.0f;
    public AIState_Attack(Character ship, AI ai)
                : base(ship, ai)
    {
        name = State.ATTACK;
    }

    public override void Enter()
    {
        target = ai.detectedEnemies[0];
        Debug.Log("Attacking.");
        character.Brake(3);
        base.Enter();


    }
    public override void Update()
    {
        if(target == null)
        {
            nextState = new AIState_Idle(character, ai);
            phase = Phase.EXIT;
            return;
        }


        character.Aim(target.transform.position);
        if (Time.time - lastFired > firingInterval)
        {
            character.photonView.RPC("Fire", RpcTarget.All);
            lastFired = Time.time;
        }

        if (Vector3.Distance(target.transform.position, character.transform.position) > ai.detectionRadius)
        {
            nextState = new AIState_Chase(character, ai);
            phase = Phase.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
