using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonFollow : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset = new Vector3(-1, 4, 2);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Camera will follow player from first person perspective

        transform.position = player.transform.position + offset;
    }
}
