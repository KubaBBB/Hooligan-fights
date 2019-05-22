using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager _weaponManager;

    public float fireRate = 15f;
    public float damage_axe = 40f;
    public float damage_fist = 10f;
    public float damage = 25f;

    private float nextTimeToFire;

    private Animator zoomCameraAnim;
    private bool zoomed;

    private Camera mainCam;

    private GameObject crosshair;

    private bool is_Aiming;

	// Use this for initialization
	void Awake () {
        _weaponManager = GetComponent<WeaponManager> ();

        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        mainCam = Camera.main;
	}

    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        WeaponShoot ();
        ZoomInAndOut();
	}

    void WeaponShoot ()
    {
        if ( _weaponManager.GetCurrentSelectedWeapon ().fireType == WeaponFireType.SINGLE )
        {
            if ( Input.GetMouseButtonDown ( 0 ) && _weaponManager.GetCurrentSelectedWeapon().IsIdle() )
            {
                if(_weaponManager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    _weaponManager.GetCurrentSelectedWeapon ().ShootAnimation ();
                }

                if ( _weaponManager.GetCurrentSelectedWeapon ().bulletType == WeaponBulletType.BULLET )
                {
                    _weaponManager.GetCurrentSelectedWeapon ().ShootAnimation ();

                    BulletFired();
                }
                else
                {
                    if (is_Aiming) 
                    {
                        _weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                        if(_weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.ARROW)
                        {
                            // throw arrow
                        }
                        else
                        {
                            // throw spead
                        }
                    }
                    //spear or bow
                }
            }

        }
        else
        {
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire )
            {
                nextTimeToFire = Time.time + 1f /fireRate;
                BulletFired();
                _weaponManager.GetCurrentSelectedWeapon ().ShootAnimation ();
            }
        }
    } // weapon shoot

    void ZoomInAndOut()
    {
        if (_weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                crosshair.SetActive(false);
            }
            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                crosshair.SetActive(true);
            }
        }

        if (_weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELF_AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _weaponManager.GetCurrentSelectedWeapon().Aim(true);
                is_Aiming = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                _weaponManager.GetCurrentSelectedWeapon().Aim(false);
                is_Aiming = false;
            }
        }
    }

    void BulletFired() {
        
        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            if (hit.transform.tag == Tags.ENEMY_TAG) {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }
    }
}
