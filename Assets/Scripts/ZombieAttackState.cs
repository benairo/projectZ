using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class ZombieAttackState : ZombieState
{
    private float _timer;
    public ZombieStateID GetID()
    {
        return ZombieStateID.Attack;
    }

    public void Enter(ZombieAgent agent)
    {
        
    }
    
    public void Update(ZombieAgent agent)
    {
        _timer = _timer - Time.deltaTime;
        float distance = Vector3.Distance(agent.playerTransform.position, agent.navMeshAgent.transform.position);
        
        if (_timer <= 0.0f)
        {
            agent.transform.Rotate(agent.playerTransform.position);
            if (!agent.animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Attack"))
            {
                if (distance > 2.0f)
                {
                    agent.stateMachine.ChangeState(ZombieStateID.ChasePlayer);

                }

                agent.animator.SetTrigger("Attack");
                Debug.Log("Attacked!");
                _timer = agent.config.attackTimer;
            }
        }
    }
    
    public void Exit(ZombieAgent agent)
    {
        
    }
}
