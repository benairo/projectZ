using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeathState : ZombieState
{
    public Vector3 direction;
    public ZombieStateID GetID()
    {
        return ZombieStateID.Death;
    }

    public void Enter(ZombieAgent agent)
    {
        agent.ragDoll.ActivateRagDoll();
        agent.waveManager.zombiesKilled++;
        agent.zombieSpawner.currentZombieAmount--;
        direction.y = 1;
        agent.ragDoll.ApplyForce(direction * agent.config.dieForce);
        agent.ui.gameObject.SetActive(false);
    }

    public void Update(ZombieAgent agent)
    {
        
    }

    public void Exit(ZombieAgent agent)
    {
        
    }
}
