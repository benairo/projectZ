using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }
    
    public bool isFiring = false;

    public int fireRate = 25;

    public float bulletSpeed = 1000.0f;

    public float bulletDrop = 0.0f;

    public ParticleSystem[] muzzleFlash;

    public ParticleSystem hitEffect;

    public Transform raycastOrigin;

    public Transform raycastDestination;

    public TrailRenderer bulletTrail;

    public string weaponName;

    private Ray _ray;

    private RaycastHit _hitInfo;
    
    private float _accumulatedTime;

    private List<Bullet> bullets = new List<Bullet>();

    private float maxLifetime = 3.0f;

    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return bullet.initialPosition + bullet.initialVelocity * bullet.time +
               0.5f * gravity * bullet.time * bullet.time;
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(bulletTrail, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
    }

    public void StartFiring()
    {
        isFiring = true;
        if (_accumulatedTime >= 0)
        {
            _accumulatedTime = 0.0f;
            FireBullet();
        }

    }

    public void UpdateFiring(float deltaTime)
    {
        float fireInterval = 1.0f / fireRate;
        while (_accumulatedTime >= 0.0f)
        {
            FireBullet();
            _accumulatedTime -= fireInterval;
        }
    }

    public void UpdateBullet(float deltaTime)
    {
        _accumulatedTime += deltaTime;
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }

    void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= maxLifetime);
    }

    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        _ray.origin = start;
        _ray.direction = direction;
        if (Physics.Raycast(_ray, out _hitInfo, distance))
        {
            hitEffect.transform.position = _hitInfo.point;
            hitEffect.transform.forward = _hitInfo.normal;
            hitEffect.Emit(1);
        
            bullet.tracer.transform.position = _hitInfo.point;
            bullet.time = maxLifetime;
        }
        else
        {
            bullet.tracer.transform.position = end;
        }
    }

    private void FireBullet()
    {
        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
