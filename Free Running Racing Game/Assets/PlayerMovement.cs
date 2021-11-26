using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Speed;
    public float JumpPower;

    private bool _jumpPressed;
    private float _horizontalInput;
    private float _verticalInput;
    private float _zAxisInput;

    private Rigidbody _rigidBody;


    // Start is called before the first frame update - initialize stuff here
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpPressed = true;
        }

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    // FixedUpdate is called once every physics update (100 times per second - 100hz default)
    void FixedUpdate()
    {

        _rigidBody.velocity = new Vector3(_horizontalInput * Speed, _rigidBody.velocity.y, _verticalInput * Speed);

        if (_jumpPressed)
        {
            _rigidBody.AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);
            _jumpPressed = false;
        }

    }
}
