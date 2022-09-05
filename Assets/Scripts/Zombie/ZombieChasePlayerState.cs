using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Interactions;
using Random = UnityEngine.Random;

public class ZombieChasePlayerState : ZombieState
{

    private float _timer = 0.0f;
    
    private float _growlTimer;
    
    private int _growlNum;
    public ZombieStateID GetID()
    {
        return ZombieStateID.ChasePlayer;
    }

    public void Enter(ZombieAgent agent)
    {
        _growlNum = Random.Range(0, agent.growlSounds.Length + 1);
        _growlTimer = Random.Range(10, 40);

    }

    public void Update(ZombieAgent agent)
    {
        _growlTimer -= Time.deltaTime;
        // Play a random growl noise every 10-40 seconds
        if (_growlTimer <= 0)
        {
            _growlTimer = 10f;
            agent.audioSource.PlayOneShot(agent.growlSounds[_growlNum]);
            
        }
        
        if (!agent.enabled)
        {
            return;
        }

        _timer -= Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.playerTransform.position;
        }
        
        // Add a timer to reduce the amount of paths created by the navmeshagent
        if (_timer < 0.0f)
        {
            Vector3 direction = agent.playerTransform.position - agent.navMeshAgent.destination;
            direction.y = 0;
            
            if (direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = agent.playerTransform.position;
                }
            }

            _timer = agent.config.maxTime;

            float distance = Vector3.Distance(agent.playerTransform.position, agent.navMeshAgent.transform.position);

            if (distance < 2.0f)
            {
                agent.stateMachine.ChangeState(ZombieStateID.Attack);
            }
        }
    }

    public void Exit(ZombieAgent agent)
    {
        
    }
}
