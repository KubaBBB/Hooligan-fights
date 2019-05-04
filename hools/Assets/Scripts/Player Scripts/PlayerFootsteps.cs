using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{

    private AudioSource _footstepSound;

    [SerializeField]
    public AudioClip [] _footstepClip;

    private CharacterController _characterController;

    [HideInInspector]
    public float _volumeMin, _volumeMax;

    private float _accumulatedDistance;

    [HideInInspector]
    public float _stepDistance;

    private void Awake ()
    {
        _footstepSound = GetComponent<AudioSource> ();
        _characterController = GetComponentInParent<CharacterController> ();
    }

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        CheckToPlayFootstepSound ();
    }

    void CheckToPlayFootstepSound ()
    {
        if ( !_characterController.isGrounded )
            return;

        if ( _characterController.velocity.sqrMagnitude > 0 )
        {
            _accumulatedDistance += Time.deltaTime;

            if ( _accumulatedDistance > _stepDistance )
            {
                _footstepSound.volume = Random.Range ( _volumeMin, _volumeMax );
                _footstepSound.clip = _footstepClip [Random.Range ( 0, _footstepClip.Length )];
                _footstepSound.Play ();

                _accumulatedDistance = 0f;

            }
        }
        else
        {
            _accumulatedDistance = 0f;
        }
    }
}
