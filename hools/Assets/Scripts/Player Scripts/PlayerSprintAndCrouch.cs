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

    private void Awake ()
    {
        _playerMovement = GetComponent<PlayerMovement> ();
        _lookRoot = transform.GetChild ( 0 );
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
        }
        else
        {
            _playerMovement.speed = _moveSpeed;
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

                _isCrouching = false;
            }
            else
            {
                _lookRoot.localPosition = new Vector3 ( 0f, _crouchHeight, 0f );
                _playerMovement.speed = _crouchSpeed;

                _isCrouching = true;
            }
        }
    }
}