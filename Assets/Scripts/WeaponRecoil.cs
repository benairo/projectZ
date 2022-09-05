using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector] public PlayerAiming playerAiming;

    [HideInInspector] public Cinemachine.CinemachineImpulseSource cameraShake;

    [HideInInspector] public Animator rigController;

    public Vector2[] recoilPattern;

    public float duration;

    public float recoilModifier = 1.0f;

    private float _verticalRecoil;
    private float _horizontalRecoil;
    private float _time;
    private int _index;

    private void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();
    }

    public void Reset()
    {
        _index = 0;
    }

    int NextIndex(int index)
    {
        return (index + 1) % recoilPattern.Length;
    }

    public void GenerateRecoil(string weaponName)
    {
        _time = duration;
        
        cameraShake.GenerateImpulse(Camera.main.transform.forward);

        _horizontalRecoil = recoilPattern[_index].x;
        _verticalRecoil = recoilPattern[_index].y;

        _index = NextIndex(_index);
        
        rigController.Play("weapon_recoil_" + weaponName, 1, 0.0f);
    }

    void Update()
    {
        if (_time > 0)
        {
            playerAiming.yAxis.Value -= (((_verticalRecoil/10) * Time.deltaTime) / duration) * recoilModifier;
            playerAiming.xAxis.Value -= (((_horizontalRecoil/10) * Time.deltaTime) / duration) * recoilModifier;
            _time -= Time.deltaTime;
        }
    }
}
