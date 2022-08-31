using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReloadWeapon : MonoBehaviour
{
    public Animator rigController;

    public WeaponAnimationEvents animationEvents;

    public ActiveWeapon activeWeapon;

    public Transform leftHand;

    public AmmoWidget ammoWidget;

    public AudioClip detachSound;

    public AudioClip attachSound;

    private AudioSource _audioSource;

    private bool _reloadAction;

    private GameObject _magazineHand;

    public void GetReloadAction(InputAction.CallbackContext context)
    {
        _reloadAction = context.performed;
        _audioSource = GetComponent<AudioSource>();
    }
    
    void Start()
    {
        animationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
    }

    void Update()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        if (weapon)
        {
            if (_reloadAction || weapon.ammoCount <= 0)
            {
                rigController.SetTrigger("reload_weapon");
            }

            if (weapon.isFiring)
            {
                ammoWidget.Refresh(weapon.ammoCount);
            }
        }
        
    }

    void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "detach_magazine":
                DetachMagazine();
                break;
            case "drop_magazine":
                DropMagazine();
                break;
            case "refill_magazine":
                RefillMagazine();
                break;
            case "attach_magazine":
                AttachMagazine();
                break;
        }
    }

    void DetachMagazine()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        _audioSource.PlayOneShot(detachSound);
        _magazineHand = Instantiate(weapon.magazine, leftHand, true);
        weapon.magazine.SetActive(false);
        
    }

    void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(_magazineHand, _magazineHand.transform.position,
            _magazineHand.transform.rotation);
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();
        _magazineHand.SetActive(false);
    }

    void RefillMagazine()
    {
        _magazineHand.SetActive(true);
    }

    void AttachMagazine()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        _audioSource.PlayOneShot(attachSound);
        weapon.magazine.SetActive(true);
        Destroy(_magazineHand);
        weapon.ammoCount = weapon.clipSize;
        rigController.ResetTrigger("reload_weapon");
        ammoWidget.Refresh(weapon.ammoCount);
    }
}
