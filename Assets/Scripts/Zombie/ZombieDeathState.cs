using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeathState : ZombieState
{
    public Vector3 direction;

    private bool _pointsGiven;
    public ZombieStateID GetID()
    {
        return ZombieStateID.Death;
    }

    public void Enter(ZombieAgent agent)
    {
        
        agent.ragDoll.ActivateRagDoll();
        direction.y = 1;
        // Prevent points being given multiple times per death
        if (!_pointsGiven)
        {
            agent.waveManager.zombiesKilled++;
            agent.zombieSpawner.currentZombieAmount--;
            agent.playerPoints.Transaction(50, true);
            _pointsGiven = true;
        }
        agent.ragDoll.ApplyForce(direction * agent.config.dieForce);
        agent.navMeshAgent.ResetPath();
    }

    public void Update(ZombieAgent agent)
    {
        
    }

    public void Exit(ZombieAgent agent)
    {
    }
}
