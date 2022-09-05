using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }

    public ActiveWeapon.WeaponSlot WeaponSlot;
    
    public bool isFiring = false;

    public int fireRate = 25;

    public float bulletSpeed = 1000.0f;

    public float bulletDrop = 0.0f;
    
    public ParticleSystem[] muzzleFlash;

    public ParticleSystem[] hitEffect;

    public Transform raycastOrigin;

    public Transform raycastDestination;

    public TrailRenderer bulletTrail;

    public string weaponName;

    public int ammoCount;

    public int mageSize;

    public int magCount;

    public WeaponRecoil recoil;

    public GameObject magazine;

    public float damage = 10;

    private AudioSource _gunShotSound;

    private Ray _ray;

    private RaycastHit _hitInfo;
    
    private float _accumulatedTime;

    private List<Bullet> bullets = new List<Bullet>();

    private float maxLifetime = 3.0f;

    [SerializeField]
    private InputActionReference shoot;
    
    private void Awake()
    {
        recoil = GetComponent<WeaponRecoil>();
        _gunShotSound = GetComponent<AudioSource>();
    }
    
    // Helper function to check if we need to reload
    public bool ShouldReload()
    {
        return ammoCount == 0 && magCount > 0;
    }
    
    // Helper function to aid with reloading ammo
    public void RefillAmmo()
    {
        ammoCount = mageSize;
        magCount--;
    }

    // Use the displacement equation to get the current position of our simulated bullet
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
        }
        recoil.Reset();
    }

    public void UpdateWeapon(float deltaTime)
    {
        if (shoot.action.WasPressedThisFrame())
        {
            StartFiring();
        }

        if (isFiring)
        {
            UpdateFiring(deltaTime);
        }

        UpdateBullet(deltaTime);
        
        if (shoot.action.WasReleasedThisFrame())
        {
            StopFiring();
        }
    }

    public void UpdateFiring(float deltaTime)
    {
        _accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while (_accumulatedTime >= 0.0f)
        {
            FireBullet();
            _accumulatedTime -= fireInterval;
        }
    }

    public void UpdateBullet(float deltaTime)
    {
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
            // If the player shoots something with a rigid body, play the blood effect.
            if (_hitInfo.rigidbody)
            {
                hitEffect[0].transform.position = _hitInfo.point;
                hitEffect[0].transform.forward = _hitInfo.normal;
                hitEffect[0].Emit(1);

            }
            else
            // Play the stone hit effect
            {
                hitEffect[1].transform.position = _hitInfo.point;
                hitEffect[1].transform.forward = _hitInfo.normal;
                hitEffect[1].Emit(1);
            }


            bullet.time = maxLifetime;
            end = _hitInfo.point;

            // Add force to the rigid body that our bullet hit
            var rb2d = _hitInfo.collider.GetComponent<Rigidbody>();
            if (rb2d)
            {
                rb2d.AddForceAtPosition(_ray.direction * 20, _hitInfo.point, ForceMode.Impulse);
            }
            
            var hitBox = _hitInfo.collider.GetComponent<HitBox>();
            if (hitBox)
            {
                hitBox.OnRaycastHit(this, _ray.direction);
            }
        }

        bullet.tracer.transform.position = end;
    }

    private void FireBullet()
    {
        if (ammoCount <= 0)
        {
            return;
        }
        ammoCount--;
        
        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);
        _gunShotSound.Play();
        recoil.GenerateRecoil(weaponName);
    }

    public void StopFiring()
    {
        isFiring = false;
    }
    
}