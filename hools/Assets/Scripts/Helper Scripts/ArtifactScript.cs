using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ArtifactScript : MonoBehaviour {

    private bool isEnable;
    public double cooldownSeconds = 25;
    private Timer cooldown;

	// Use this for initialization
	void Start () {
        cooldown = new Timer ( cooldownSeconds );
	}
	
	// Update is called once per frame
	void Update () {
        if ( isEnable )
        {
            transform.RotateAround ( Vector3.zero, Vector3.up, 20 * Time.deltaTime );
            //if()
        }
        else
        {


        }
    }
}
