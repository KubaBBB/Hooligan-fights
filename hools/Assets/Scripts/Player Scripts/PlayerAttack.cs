using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager _weaponManager;

    public float fireRate = 15f;
    public float damage = 20f;

    private float nextTimeToFire;

	// Use this for initialization
	void Start () {
        _weaponManager = GetComponent<WeaponManager> ();
	}
	
	// Update is called once per frame
	void Update () {
        WeaponShoot ();
	}

    void WeaponShoot ()
    {
        if ( _weaponManager.GetCurrentSelectedWeapon ().fireType == WeaponFireType.SINGLE )
        {
            if ( Input.GetMouseButtonDown ( 0 ) )
            {
                if(_weaponManager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    _weaponManager.GetCurrentSelectedWeapon ().ShootAnimation ();
                }

                if ( _weaponManager.GetCurrentSelectedWeapon ().bulletType == WeaponBulletType.BULLET )
                {
                    _weaponManager.GetCurrentSelectedWeapon ().ShootAnimation ();

                    //BulletFired();
                }
                else
                {
                    //spear or bow
                }
            }

        }
        else
        {
            if(Input.GetMouseButton(0) && Time.time > nextTimeToFire )
            {
                nextTimeToFire = Time.time + 1f /fireRate;

                _weaponManager.GetCurrentSelectedWeapon ().ShootAnimation ();
            }
        }
    }
}
