using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    private Camera _mainCamera;
    private Ray _ray;
    private RaycastHit _hitInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _ray.origin = _mainCamera.transform.position;
        _ray.direction = _mainCamera.transform.forward;
        Physics.Raycast(_ray, out _hitInfo);
        transform.position = _hitInfo.point;
    }
}
