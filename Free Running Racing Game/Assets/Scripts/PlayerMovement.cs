using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float Speed;
    [SerializeField] private float JumpPower;
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private string[] DirectionKeys;
    [SerializeField] private string JumpKey;

    private bool _jumpPressed;
    private float _horizontalInput;
    private float _verticalInput;


    private Rigidbody _rigidBody;


    // Start is called before the first frame update - initialize stuff here
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(DirectionKeys[0]))
        {
            _verticalInput = 1.0f;
        }

        else if (Input.GetKeyUp(DirectionKeys[0]))
        {
            _verticalInput = 0.0f;
        }

        if (Input.GetKeyDown(DirectionKeys[1]))
        {
            _verticalInput = -1.0f;
        }

        else if (Input.GetKeyUp(DirectionKeys[1]))
        {
            _verticalInput = 0.0f;
        }

        if (Input.GetKeyDown(DirectionKeys[2]))
        {
            _horizontalInput = -1.0f;
        }

        else if (Input.GetKeyUp(DirectionKeys[2]))
        {
            _horizontalInput = 0.0f;
        }

        if (Input.GetKeyDown(DirectionKeys[3]))
        {
            _horizontalInput = 1.0f;
        }

        else if (Input.GetKeyUp(DirectionKeys[3]))
        {
            _horizontalInput = 0.0f;
        }


        if (Input.GetKeyDown(JumpKey))
        {
            _jumpPressed = true;
        }
    }

    // FixedUpdate is called once every physics update (100 times per second - 100hz default)
    void FixedUpdate()
    {

        _rigidBody.velocity = new Vector3(_horizontalInput * Speed, _rigidBody.velocity.y, _verticalInput * Speed);

        if (Physics.OverlapSphere(_groundCheckTransform.position, 0.2f, _playerMask).Length == 0)
        {
            // not colliding with anything so player is in the air - so prevent from jumping again 
            return;
        }

        if (_jumpPressed)
        {
            _rigidBody.AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);
            _jumpPressed = false;
        }

    }
}
