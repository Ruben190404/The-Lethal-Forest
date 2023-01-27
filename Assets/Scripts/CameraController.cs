using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    PlayerMovement playerMovement;
    float sensitivity;
    
    private float rotationX = 0f;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        sensitivity = playerMovement.CameraSensitivity;
    }


    void Update()
    {
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        
        rotationX = Mathf.Clamp(rotationX, -90, 0f);
        
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}