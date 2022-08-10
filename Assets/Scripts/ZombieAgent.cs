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

    public Animator animator;

    public PlayerHealth playerHealth;
    void Start()
    {
        ragDoll = GetComponent<RagDoll>();
        ui = GetComponentInChildren<UIHealthBar>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerHealth = playerTransform.GetComponent<PlayerHealth>();
        stateMachine = new ZombieStateMachine(this);
        stateMachine.RegisterState(new ZombieChasePlayerState());
        stateMachine.RegisterState(new ZombieDeathState());
        stateMachine.RegisterState(new ZombieIdleState());
        stateMachine.RegisterState(new ZombieAttackState());
        stateMachine.ChangeState(initialState);
    }

    void Update()
    {
        stateMachine.Update();
    }
}
