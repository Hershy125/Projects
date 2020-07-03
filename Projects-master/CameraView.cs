using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{

    public Camera firstPersonCam;

    public Camera thirdPersonCam;

    public KeyCode key = KeyCode.C;
    public bool camSwitch = false;
    
    // Start is called before the first frame update
    void Start()
    {
        firstPersonCam.gameObject.SetActive(false);
        thirdPersonCam.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Press key to change camera view from third person to first person and back.

        if (Input.GetKeyDown(key))
        {
            camSwitch = !camSwitch;

            firstPersonCam.gameObject.SetActive(camSwitch);
            thirdPersonCam.gameObject.SetActive(!camSwitch);

            Debug.Log("Camera View has been changed");
        }

    }
}
