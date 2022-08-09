using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieLocomotion : MonoBehaviour
{
    public Transform playerTransform;

    public float maxTime = 1.0f;

    public float maxDistance = 1.0f;
    
    private NavMeshAgent _agent;
    
    private Animator _animator;

    private float _timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0.0f)
        {
            float distance = (playerTransform.position - _agent.destination).sqrMagnitude;
            if (distance > maxDistance * maxDistance)
            {
                _agent.destination = playerTransform.position;

            }
            _timer = maxTime;
        }
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }
}
