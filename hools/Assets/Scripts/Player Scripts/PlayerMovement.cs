using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;

    private Vector3 _moveDirection;

    private float _gravity = 9.81f;
    private float _verticalVelocity;

    public float speed = 5f;
    public float jumpForce = 4;
    
    private void Awake ()
    {
        _characterController = GetComponent<CharacterController> ();
    }

	// Update is called once per frame
	void Update () {
        MoveThePlayer ();	
	}

    void MoveThePlayer ()
    {
        _moveDirection = new Vector3 ( Input.GetAxis ( Axis.HORIZONTAL ), 
            0f,
            Input.GetAxis ( Axis.VERTICAL ) );

        _moveDirection = transform.TransformDirection ( _moveDirection );

        _moveDirection *= speed*Time.deltaTime;

        ApplyGravity ();

        _characterController.Move ( _moveDirection );
    }

    void ApplyGravity ()
    {
        if ( _characterController.isGrounded )
        {
            _verticalVelocity -= _gravity * Time.deltaTime;

            PlayerJump ();
        }
        else
        {
            _verticalVelocity -= _gravity * Time.deltaTime;
        }

        _moveDirection.y = _verticalVelocity * Time.deltaTime;
    }

    void PlayerJump ()
    {
        if(_characterController.isGrounded && Input.GetKeyDown ( KeyCode.Space ) )
        {
            _verticalVelocity = jumpForce;
        }
    }
}
