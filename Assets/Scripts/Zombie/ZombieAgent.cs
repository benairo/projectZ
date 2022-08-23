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

    public WaveManager waveManager;

    public ZombieSpawner zombieSpawner;
    private void Start()
    {
        ragDoll = GetComponent<RagDoll>();
        ui = GetComponentInChildren<UIHealthBar>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHealth = playerTransform.GetComponent<PlayerHealth>();
        waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
        zombieSpawner = waveManager.GetComponentInChildren<ZombieSpawner>();
        stateMachine = new ZombieStateMachine(this);
        stateMachine.RegisterState(new ZombieChasePlayerState());
        stateMachine.RegisterState(new ZombieDeathState());
        stateMachine.RegisterState(new ZombieIdleState());
        stateMachine.RegisterState(new ZombieAttackState());
        stateMachine.ChangeState(initialState);
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
