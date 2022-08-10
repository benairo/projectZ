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
            Vector3 direction = (agent.playerTransform.position - agent.navMeshAgent.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            agent.navMeshAgent.transform.rotation = Quaternion.Slerp(agent.navMeshAgent.transform.rotation,
                lookRotation, Time.deltaTime * 2.0f);
            if (!agent.animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Attack"))
            {
                if (distance > 2.0f)
                {
                    agent.stateMachine.ChangeState(ZombieStateID.ChasePlayer);

                }

                agent.animator.SetTrigger("Attack");
                Vector3 noDirection = Vector3.zero;
                agent.playerHealth.TakeDamage(agent.config.damageAmount, noDirection);
                if (agent.playerHealth.IsDead())
                {
                    agent.stateMachine.ChangeState(ZombieStateID.Idle);
                }
                _timer = agent.config.attackTimer;
            }
        }
    }
    
    
    public void Exit(ZombieAgent agent)
    {
        
    }
}
