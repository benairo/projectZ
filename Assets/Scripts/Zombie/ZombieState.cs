using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ZombieStateID 
{
    ChasePlayer,
    Death,
    Idle,
    Attack
}

public interface ZombieState 
{
    ZombieStateID GetID(); 
    void Enter(ZombieAgent agent); 
    void Update(ZombieAgent agent); 
    void Exit(ZombieAgent agent);
}
