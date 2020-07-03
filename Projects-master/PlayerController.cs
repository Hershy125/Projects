using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 15;
    private float turnSpeed = 25;
    private float horizontalInput;
    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Axis setup

        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        
        // This code moves the vehicle forward.

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        // Rotate left and right
        transform.Rotate(Vector3.up, (turnSpeed * horizontalInput * Time.deltaTime));
        
        
    }
}
