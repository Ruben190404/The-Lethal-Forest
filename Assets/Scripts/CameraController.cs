using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    // PlayerMovement playerMovement;
    // float sensitivity;
    //
    // private float rotationX = 0f;
    //
    // void Start()
    // {
    //     playerMovement = GetComponentInParent<PlayerMovement>();
    //     sensitivity = playerMovement.CameraSensitivity;
    // }
    //
    //
    // void Update()
    // {
    //     rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
    //     
    //     rotationX = Mathf.Clamp(rotationX, -90, 0f);
    //     
    //     transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    //     
    // }
    void OnPreCull()
    {
        GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);

        var aspect = (float)Screen.width / (float)Screen.height;
        var scale = Mathf.Max(1.0f / aspect, 1.0f);

        GetComponent<Camera>().rect = new Rect(0, 0, scale, scale);
    }
}
