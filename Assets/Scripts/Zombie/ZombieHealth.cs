using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : Health
{
    private ZombieAgent _agent;
    
    protected override void OnStart()
    {
        _agent = GetComponent<ZombieAgent>();
    }

    protected override void OnDamage(Vector3 direction)
    {
    }

    protected override void OnDeath(Vector3 direction)
    {
        ZombieDeathState deathState = _agent.stateMachine.GetState(ZombieStateID.Death) as ZombieDeathState;
        deathState.direction = direction;
        _agent.stateMachine.ChangeState(ZombieStateID.Death);
    }
}
