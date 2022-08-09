using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAgent : MonoBehaviour
{
    public ZombieStateMachine stateMachine;

    public ZombieStateID initialState;

    public NavMeshAgent navMeshAgent;

    public ZombieAgentConfig config;

    public RagDoll ragDoll;

    public UIHealthBar ui;
    
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        ragDoll = GetComponent<RagDoll>();
        ui = GetComponentInChildren<UIHealthBar>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new ZombieStateMachine(this);
        stateMachine.RegisterState(new ZombieChasePlayerState());
        stateMachine.RegisterState(new ZombieDeathState());
        stateMachine.RegisterState(new ZombieIdleState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
