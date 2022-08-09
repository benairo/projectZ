using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChasePlayerState : ZombieState
{

    private float _timer = 0.0f;
    public ZombieStateID GetID()
    {
        return ZombieStateID.ChasePlayer;
    }

    public void Enter(ZombieAgent agent)
    {
        if (agent.playerTransform == null)
        {
            agent.playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void Update(ZombieAgent agent)
    {
        if (!agent.enabled)
        {
            return;
        }

        _timer -= Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.playerTransform.position;
        }

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
        }
    }

    public void Exit(ZombieAgent agent)
    {
        
    }
}
