using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateMachine
{
    public ZombieState[] states;
    public ZombieAgent agent;
    public ZombieStateID currentState;

    public ZombieStateMachine(ZombieAgent agent)
    {
        this.agent = agent;
        int numStates = System.Enum.GetNames(typeof(ZombieStateID)).Length;
        states = new ZombieState[numStates];
    }

    public void RegisterState(ZombieState state)
    {
        int index = (int) state.GetID();
        states[index] = state;
    }

    public ZombieState GetState(ZombieStateID stateID)
    {
        int index = (int)stateID;
        return states[index];
    }

    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }

    public void ChangeState(ZombieStateID newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
