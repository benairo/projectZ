using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class PlayerAiming : MonoBehaviour
{
    public float turnSpeed = 15;

    public float aimDuration = 0.3f;

    public Transform cameraPoint;

    public Cinemachine.AxisState xAxis;

    public Cinemachine.AxisState yAxis;

    private Cinemachine.CinemachineInputProvider _inputAxisProvider;

    private PlayerInput _playerInput;

    private Camera _camera;

    private InputAction _aimAction;
    
    
    // Start is called before the first frame update
    
    void Start()
    {
        _inputAxisProvider = GetComponent<Cinemachine.CinemachineInputProvider>();
        xAxis.SetInputAxisProvider(0, _inputAxisProvider);
        yAxis.SetInputAxisProvider(1, _inputAxisProvider);
        _playerInput = GetComponent<PlayerInput>();
        _aimAction = _playerInput.actions["Aim"];
        _camera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);

        cameraPoint.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
        
        float yawCamera = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {

    }
}
