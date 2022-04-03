using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour
{
    public Transform ResetDestination = null;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -500)
        {
            if (ResetDestination)
            {
                transform.position = ResetDestination.transform.position;
            }
            else
            {
                transform.position = new Vector3(0, 5, 0);
            }

            transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        }
    }
}
