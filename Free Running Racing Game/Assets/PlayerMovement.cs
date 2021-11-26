using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Speed;
    public float JumpPower;

    private Rigidbody rb;

    void Start()
    {
          rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }

        else if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.back * Speed * Time.deltaTime);
        }

        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }

        else if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }

        if (Input.GetKey("space"))
        {
            rb.AddForce(Vector3.up * JumpPower);
        }
    }
}
