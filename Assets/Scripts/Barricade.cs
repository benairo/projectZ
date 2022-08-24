using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Barricade : MonoBehaviour
{
    public int pointCost;

    public GameObject barricadePrompt;

    private PlayerPoints _playerPoints;

    private bool _buttonPressed;

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
            barricadePrompt.gameObject.SetActive(true);
            if (_buttonPressed && _playerPoints.CheckTransaction(pointCost))
            {
                _playerPoints.Transaction(pointCost, false);
                barricadePrompt.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            barricadePrompt.gameObject.SetActive(false);
        }
    }
}
