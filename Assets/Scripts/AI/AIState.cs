using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AIState
{
    public enum State
    {
        IDLE, PATROL, CHASE, ATTACK
    };

    public enum Phase
    {
        ENTER, UPDATE, EXIT
    };

    public State name; 
    protected Phase phase;
    protected Ship ship;
    protected AI ai;
    protected AIState nextState;

    public AIState(Ship ship, AI ai)
    {
        this.ship = ship;
        this.ai = ai;
        phase = Phase.ENTER;
    }

    public virtual void Enter() { phase = Phase.UPDATE; } // Runs first whenever you come into a state and sets the phase to whatever is next, so it will know later on in the process where it's going.
    public virtual void Update() { phase = Phase.UPDATE; } // Once you are in UPDATE, you want to stay in UPDATE until it throws you out.
    public virtual void Exit() { phase = Phase.EXIT; } // Uses EXIT so it knows what to run and clean up after itself.

    public AIState Process()
    {
        if (phase == Phase.ENTER) Enter();
        if (phase == Phase.UPDATE) Update();
        if (phase == Phase.EXIT)
        {
            Exit();
            return nextState; // Notice that this method returns a 'state'.
        }
        return this; // If we're not returning the nextState, then return the same state.
    }

    protected void DetectShips()
    {
        //TODO: Optmize this check
        Collider2D[] cols = Physics2D.OverlapCircleAll(ship.transform.position, ai.detectionRadius, LayerMask.GetMask("Ships"));
        foreach (Collider2D col in cols)
        {
            Ship s = col.gameObject.GetComponent<Ship>();
            //TODO:
            if (s!= null)
            {
                if(s.shipData.team != ship.shipData.team)
                {
                    if(!ai.detectedEnemies.Contains(s))
                        ai.detectedEnemies.Add(s);
                }
                else
                {
                    if (!ai.detectedAllies.Contains(s))
                        ai.detectedAllies.Add(s);
                }
                
            }
        }
    }
}
