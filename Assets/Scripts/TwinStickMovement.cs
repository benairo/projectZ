using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class TwinStickMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;

    [SerializeField] private float gravityValue = -9.81f;

    [SerializeField] private float controllerDeadzone = 0.1f;

    [SerializeField] private float gamepadRotateSmoothing = 1000f;

    [SerializeField] private bool isGamePad;

    private CharacterController _controller;

    private Animator _animator;

    private Vector2 _movement;

    private Vector2 _aim;

    private Vector3 _playerVelocity;

    private PlayerControls _playerControls;

    private PlayerInput _playerInput;

    private Transform _cam;

    private Vector3 _camForward;

    private Vector3 _move;

    private Vector3 _moveInput;

    private float _forwardAmount;

    private float _turnAmount;
    // Start is called before the first frame update
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerControls = new PlayerControls();
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _cam = Camera.main.transform;

    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
    }

    void HandleInput()
    {
        _movement = _playerControls.Controls.Movement.ReadValue<Vector2>();
        _aim = _playerControls.Controls.Aim.ReadValue<Vector2>();
    }

    void HandleMovement()
    {
        Vector3 move = new Vector3(_movement.x, 0, _movement.y);
        move.Normalize();
        _controller.Move(move * Time.deltaTime * playerSpeed);

        if (_cam != null)
        {
            _camForward = Vector3.Scale(_cam.up, new Vector3(1, 0, 1)).normalized;
            _move = _movement.x * _camForward + _movement.y * _cam.right;
        }
        else
        {
            _move = _movement.x * _camForward + _movement.y * _cam.right;
        }

        if (_move.magnitude > 1)
        {
            move.Normalize();
        }

        Move(_move);
        
        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    void Move(Vector3 move)
    {
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        this._moveInput = move;

        ConvertMoveInput();
        UpdateAnimator();
    }

    void ConvertMoveInput()
    {
        print(_moveInput);
        Vector3 localMove = transform.InverseTransformDirection(_moveInput);
        print(localMove);
        _turnAmount = localMove.x;
        _forwardAmount = localMove.z;
    }

    void UpdateAnimator()
    {
        _animator.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
        _animator.SetFloat("Turn", _turnAmount, 0.1f, Time.deltaTime);
    }

    void HandleRotation()
    {
        if (isGamePad)
        {
            if (Mathf.Abs(_aim.x) > controllerDeadzone || Mathf.Abs(_aim.y) > controllerDeadzone)
            {
                Vector3 playerDirection = Vector3.right * _aim.x + Vector3.forward * _aim.y;
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation,
                        gamepadRotateSmoothing * Time.deltaTime);
                }
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(_aim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
            }
        }
    }

    private void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamePad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }
}
