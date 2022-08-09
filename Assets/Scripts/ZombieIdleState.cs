using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : ZombieState
{
    public ZombieStateID GetID()
    {
        return ZombieStateID.Idle;
    }

    public void Enter(ZombieAgent agent)
    {
        
    }
    
    public void Update(ZombieAgent agent)
    {
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        if (playerDirection.magnitude > agent.config.maxSightDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;
        
        playerDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        if (dotProduct > 0.0f)
        {
            agent.stateMachine.ChangeState(ZombieStateID.ChasePlayer);
        }
    }
    
    public void Exit(ZombieAgent agent)
    {
        
    }
}
