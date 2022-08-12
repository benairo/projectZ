using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;


public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
    }
    public Transform crosshairTarget;

    public UnityEngine.Animations.Rigging.Rig handIK;

    public Transform[] weaponSlots;

    public Animator rigController;

    public PlayerAiming playerAiming;
    
    public AmmoWidget ammoWidget;

    private RaycastWeapon[] _equippedWeapons = new RaycastWeapon[2];
    
    private int _activeWeaponIndex;
    
    private bool _isHolstered = false;
    
    // Start is called before the first frame update
    void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            Equip(existingWeapon);
        }
    }

    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeapon(_activeWeaponIndex);
    }

    RaycastWeapon GetWeapon(int index)
    {
        if (index < 0 || index >= _equippedWeapons.Length)
        {
            return null;
        }
        return _equippedWeapons[index];
    }

    public void GetHolsterInput(InputAction.CallbackContext context)
    {
        ToggleActiveWeapon();
    }

    public void GetPrimaryWeaponInput(InputAction.CallbackContext context)
    {
        SetActiveWeapon(WeaponSlot.Primary);
    }
    public void GetSecondaryWeaponInput(InputAction.CallbackContext context)
    {
        SetActiveWeapon(WeaponSlot.Secondary);
    }

    // Update is called once per frame
    void Update()
    {
        var weapon = GetWeapon(_activeWeaponIndex);
        if (weapon && !_isHolstered)
        {
            weapon.UpdateWeapon(Time.deltaTime);
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.WeaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.raycastDestination = crosshairTarget;
        weapon.recoil.playerAiming = playerAiming;
        weapon.recoil.rigController = rigController;
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        _equippedWeapons[weaponSlotIndex] = weapon;
        _activeWeaponIndex = weaponSlotIndex;
        
        SetActiveWeapon(newWeapon.WeaponSlot);
        
        ammoWidget.Refresh(weapon.ammoCount);
    }

    void ToggleActiveWeapon()
    {
        bool isHolstered = rigController.GetBool("holster_weapon");
        if (isHolstered)
        {
            StartCoroutine(ActivateWeapon(_activeWeaponIndex));
        }
        else
        {
            StartCoroutine(HolsterWeapon(_activeWeaponIndex));
        }
    }

    void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = _activeWeaponIndex;
        int activateIndex = (int)weaponSlot;

        if (holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }
        
        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        _activeWeaponIndex = activateIndex;
    }
    
    IEnumerator HolsterWeapon(int index)
    {
        _isHolstered = true;
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holster_weapon", true);
            do
            {
                yield return new WaitForEndOfFrame();
            } 
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
    }
    
    IEnumerator ActivateWeapon(int index)
    {
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + weapon.weaponName);
            do
            {
                yield return new WaitForEndOfFrame();
            } 
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

            _isHolstered = false;
            weapon.StopFiring();
        }
    }
}