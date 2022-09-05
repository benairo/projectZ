using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class WeaponPurchase : MonoBehaviour
{
    public int cost;
    
    public RaycastWeapon weaponPrefab;

    public GameObject weaponPrompt;
    
    private bool _buttonPressed;
    
    private PlayerPoints _playerPoints;

    private void Awake()
    {
        _playerPoints = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerPoints>();
    }
    
    public void GetInteractInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _buttonPressed = true;
        }
        else
        {
            _buttonPressed = false;
        }
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            weaponPrompt.gameObject.SetActive(true);
            if (_buttonPressed && _playerPoints.CheckTransaction(cost)) 
            {
                _playerPoints.Transaction(cost, false);
                ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
                if (activeWeapon)
                {
                    RaycastWeapon newWeapon = Instantiate(weaponPrefab);
                    activeWeapon.Equip(newWeapon);
                }
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            weaponPrompt.gameObject.SetActive(false);
        }
    }
}
