using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    public float rotationSpeed;
    PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");

        if (playerController.hasSpeedPowerup)
        {
            rotationSpeed = 65f;
        }
        else
        {
            rotationSpeed = 40f;
        }

        transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime); 
    }
}
