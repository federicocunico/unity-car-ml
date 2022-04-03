using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    //public bool UsePhysic = true;
    public float Speed = 20f;
    public float RotationSpeed = 5f;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Rotate left
        if (Input.GetKey(KeyCode.A))
        {
            //_rb.AddForce(Vector3.left * RotationSpeed);


            //Quaternion deltaRotation = Quaternion.Euler(Vector3.down * RotationSpeed);
            // _rb.MoveRotation(_rb.rotation * deltaRotation);


            transform.Rotate(0, -RotationSpeed, 0);

        }

        // Rotate right
        if (Input.GetKey(KeyCode.D))
        {
            //_rb.AddForce(Vector3.right * RotationSpeed);


            //Quaternion deltaRotation = Quaternion.Euler(Vector3.up * RotationSpeed);
            //_rb.MoveRotation(_rb.rotation * deltaRotation);

            transform.Rotate(0, +RotationSpeed, 0);
        }

        // Forward
        if (Input.GetKey(KeyCode.W))
        {
            // _rb.AddForce(transform.forward * Speed);

            transform.Translate(0, 0, Speed);
        }

        // Backward
        if (Input.GetKey(KeyCode.S))
        {
            // _rb.AddForce(transform.forward * Speed);

            transform.Translate(0, 0, -Speed);
        }
    }
}




/*
 if (Input.GetKey(KeyCode.UpArrow))
        {
            var moveDir = Vector3.forward * Time.deltaTime;

            if (UsePhysic)
            {
                _rb.AddForce(moveDir * Speed);
            }
            else
            {
                transform.Translate(moveDir);
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            var moveDir = Vector3.back * Time.deltaTime;
            if (UsePhysic)
            {
                _rb.AddForce(moveDir * Speed);
            }
            else
            {
                transform.Translate(moveDir);
            }

        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (UsePhysic)
            {
                Debug.Log("Not implemented, using transform.Rotate()");
                transform.Rotate(Vector3.up, -RotationSpeed);
            }
            else
            {
                transform.Rotate(Vector3.up, -RotationSpeed);
            }

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (UsePhysic)
            {
                Debug.Log("Not implemented, using transform.Rotate()");
                transform.Rotate(Vector3.up, RotationSpeed);
            }
            else
            {
                transform.Rotate(Vector3.up, RotationSpeed);
            }

        }
 */