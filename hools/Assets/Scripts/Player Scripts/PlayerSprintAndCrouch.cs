using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    public float _sprintSpeed = 10f;
    public float _moveSpeed = 5f;
    public float _crouchSpeed = 2f;

    private Transform _lookRoot;

    private float _standHeight = 1.6f;
    private float _crouchHeight = 1f;

    private bool _isCrouching;

    private PlayerFootsteps _playerFootsteps;

    private float _sprintVolume = 1f;
    private float _crouchVolume = 0.1f;
    private float _walkVolumeMin = 0.2f, _walkVolumeMax = 0.6f;

    private float _walkStepDistance = 0.4f;
    private float _sprintStepDistnace = 0.25f;
    private float _crouchStepDistance = 0.5f;

    private void Awake ()
    {
        _playerMovement = GetComponent<PlayerMovement> ();
        _lookRoot = transform.GetChild ( 0 );

        _playerFootsteps = GetComponentInChildren<PlayerFootsteps> ();
    }

    private void Start ()
    {
        _playerFootsteps._volumeMin = _walkVolumeMin;
        _playerFootsteps._volumeMax = _walkVolumeMax;
        _playerFootsteps._stepDistance = _walkStepDistance;
    }

    // Update is called once per frame
    void Update ()
    {
        Sprint ();
        Crouch ();
    }

    void Sprint ()
    {
        if ( Input.GetKeyDown ( KeyCode.LeftShift ) && !_isCrouching )
        {
            _playerMovement.speed = _sprintSpeed;

            _playerFootsteps._stepDistance = _sprintStepDistnace;
            _playerFootsteps._volumeMin = _sprintVolume;
            _playerFootsteps._volumeMax = _sprintVolume;
        }

        if ( Input.GetKeyUp ( KeyCode.LeftShift ) && !_isCrouching )
        {
            _playerMovement.speed = _moveSpeed;

            _playerFootsteps._stepDistance = _walkStepDistance;
            _playerFootsteps._volumeMin = _walkVolumeMin;
            _playerFootsteps._volumeMax = _walkVolumeMax;
        }
    }

    void Crouch ()
    {
        if ( Input.GetKeyDown ( KeyCode.C ) )
        {
            if ( _isCrouching )
            {
                _lookRoot.localPosition = new Vector3 ( 0f, _standHeight, 0f );
                _playerMovement.speed = _moveSpeed;

                _playerFootsteps._volumeMin = _walkVolumeMin;
                _playerFootsteps._volumeMax = _walkVolumeMax;
                _playerFootsteps._stepDistance = _walkStepDistance;

                _isCrouching = false;
            }
            else
            {
                _lookRoot.localPosition = new Vector3 ( 0f, _crouchHeight, 0f );
                _playerMovement.speed = _crouchSpeed;

                _playerFootsteps._stepDistance = _crouchStepDistance;
                _playerFootsteps._volumeMin = _crouchVolume;
                _playerFootsteps._volumeMax = _crouchVolume;

                _isCrouching = true;
            }
        }
    }
}