using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerAiming : MonoBehaviour
{
    public float turnSpeed = 15;
    
    public Transform cameraPoint;

    public Cinemachine.AxisState xAxis;

    public Cinemachine.AxisState yAxis;

    private Cinemachine.CinemachineInputProvider _inputAxisProvider;
    
    private Camera _camera;

    private InputAction _aimAction;

    private Animator _animator;

    private ActiveWeapon _activeWeapon;

    private int _isAimingParam = Animator.StringToHash("isAiming");

    private bool _isAiming;
    
    void Start()
    {
        _inputAxisProvider = GetComponent<Cinemachine.CinemachineInputProvider>();
        xAxis.SetInputAxisProvider(0, _inputAxisProvider);
        yAxis.SetInputAxisProvider(1, _inputAxisProvider);
        _camera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _animator = GetComponent<Animator>();
        _activeWeapon = GetComponent<ActiveWeapon>();
    }

    public void AimingPerformed(InputAction.CallbackContext context)
    {
        _isAiming = context.performed;
        _animator.SetBool(_isAimingParam, _isAiming);

        var weapon = _activeWeapon.GetActiveWeapon();
        if (weapon)
        {
            weapon.recoil.recoilModifier = _isAiming ? 0.35f : 1.0f;
        }
    }
    void FixedUpdate()
    {
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);

        cameraPoint.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
        
        float yawCamera = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }
}
