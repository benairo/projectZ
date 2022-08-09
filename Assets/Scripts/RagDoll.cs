using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll : MonoBehaviour
{
    
    private Rigidbody[] _rigidBodies;
    private Animator _animator;
    void Start()
    {
        _rigidBodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        DeactivateRagDoll();
    }

    public void DeactivateRagDoll()
    {
        foreach (var rigidBody in _rigidBodies)
        {
            rigidBody.isKinematic = true;
        }

        _animator.enabled = true;
    }

    public void ActivateRagDoll()
    {
        foreach (var rigidBody in _rigidBodies)
        {
            rigidBody.isKinematic = false;
        }

        _animator.enabled = false;
    }

    public void ApplyForce(Vector3 force)
    {
        var rigidBody = _animator.GetBoneTransform(HumanBodyBones.Head).GetComponent<Rigidbody>();
        if (rigidBody)
        {
            rigidBody.AddForce(force, ForceMode.VelocityChange);
        }
    }
}
