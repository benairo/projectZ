using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;


public class ActiveWeapon : MonoBehaviour
{
    public Transform crosshairTarget;

    public UnityEngine.Animations.Rigging.Rig handIK;

    public Transform weaponParent;

    public Transform weaponLeftGrip;

    public Transform weaponRightGrip;
    
    public Animator rigController;

    private RaycastWeapon _weapon;
    
    private PlayerInput _playerInput;

    private InputAction _shootAction;

    private InputAction _holsterAction;
    // Start is called before the first frame update
    void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            Equip(existingWeapon);
        }
        _playerInput = GetComponent<PlayerInput>();
        _shootAction = _playerInput.actions["Shoot"];
        _holsterAction = _playerInput.actions["Holster"];
    }

    // Update is called once per frame
    void Update()
    {
        if (_weapon)
        {
            // FIND A FIX FOR THIS!!!!!!!!!!!!
            handIK.weight = 1.0f;
            
            if (_shootAction.IsPressed())
            {
                _weapon.StartFiring();
            }

            if (_weapon.isFiring)
            {
                _weapon.UpdateFiring(Time.deltaTime);
            }
            _weapon.UpdateBullet(Time.deltaTime);
            if (!_shootAction.IsPressed())
            {
                _weapon.StopFiring();
            }

            if (_holsterAction.triggered)
            {
                bool isHolstered = rigController.GetBool("holster_weapon");
                rigController.SetBool("holster_weapon", !isHolstered);
            }
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        if (_weapon)
        {
            Destroy(_weapon.gameObject);
        }
        _weapon = newWeapon;
        _weapon.raycastDestination = crosshairTarget;
        _weapon.transform.parent = weaponParent;
        _weapon.transform.localPosition = Vector3.zero;
        _weapon.transform.localRotation = Quaternion.identity;
        rigController.Play("equip_" + _weapon.weaponName);
    }
}
