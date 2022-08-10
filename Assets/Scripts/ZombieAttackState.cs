using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class ZombieAttackState : ZombieState
{
    public ZombieStateID GetID()
    {
        return ZombieStateID.Attack;
    }

    public void Enter(ZombieAgent agent)
    {
        
    }
    
    public void Update(ZombieAgent agent)
    {
        agent.animator.SetTrigger("Attack");
        if (!agent.animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Attack"))
        {
            agent.stateMachine.ChangeState(ZombieStateID.ChasePlayer);
        }
    }
    
    public void Exit(ZombieAgent agent)
    {
        
    }
}
