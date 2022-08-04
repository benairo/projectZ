using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public bool isFiring = false;

    public ParticleSystem[] muzzleFlash;

    public ParticleSystem hitEffect;

    public Transform raycastOrigin;

    public Transform raycastDestination;

    public TrailRenderer bulletTrail;

    private Ray _ray;

    private RaycastHit _hitInfo;

    public void StartFiring()
    {
        isFiring = true;
        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        _ray.origin = raycastOrigin.position;
        _ray.direction = raycastDestination.position - raycastOrigin.position;

        var tracer = Instantiate(bulletTrail, _ray.origin, Quaternion.identity);
        tracer.AddPosition(_ray.origin);
        
        if (Physics.Raycast(_ray, out _hitInfo))
        {
            hitEffect.transform.position = _hitInfo.point;
            hitEffect.transform.forward = _hitInfo.normal;
            hitEffect.Emit(1);

            tracer.transform.position = _hitInfo.point;
        }
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
