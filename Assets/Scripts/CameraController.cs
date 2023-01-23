using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
       PlayerMovement PlayerMovement;
       float sensitivity;
    void Start()
    {
        PlayerMovement = GetComponentInParent<PlayerMovement>();
        sensitivity = PlayerMovement.CameraSensitivity;
        Debug.Log(sensitivity);
    }

    // Update is called once per frame
    void Update()
    {
        // float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        // transform.Rotate(-mouseY, 0,0);   
        
        //when the mouse moves on the y axis rotate the camera around the player object
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        transform.RotateAround(transform.parent.position, transform.right, -mouseY);
    }
}
