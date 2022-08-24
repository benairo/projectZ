using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public float stepDown;
    
    private PlayerControls _playerControls;

    private PlayerInput _playerInput;
    
    private Animator _animator;
    
    private Vector2 _input;

    private bool _isGamePad;

    private Vector3 _rootMotion;

    private CharacterController _charController;
    
    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _charController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void FixedUpdate()
    {
        _input = _playerControls.Controls.Movement.ReadValue<Vector2>();
        
        _animator.SetFloat("InputX", _input.x);
        _animator.SetFloat("InputY", _input.y);

        _charController.Move(_rootMotion + Vector3.down * stepDown);
        _rootMotion = Vector3.zero;

    }
    public void OnDeviceChange(PlayerInput pi)
    {
        _isGamePad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }

    private void OnAnimatorMove()
    {
        _rootMotion += _animator.deltaPosition;
    }
}
