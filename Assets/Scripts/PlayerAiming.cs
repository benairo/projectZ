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

    private PlayerInput _playerInput;

    private Camera _camera;

    private InputAction _aimAction;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _aimAction = _playerInput.actions["Aim"];
        _camera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float yawCamera = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        // if (_aimAction.IsPressed())
        // {
        //     aimLayer.weight += Time.deltaTime / aimDuration;
        // }
        // else
        // {
        //     aimLayer.weight -= Time.deltaTime / aimDuration;
        // }

    }
}
