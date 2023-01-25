using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider bc;
    private float Speed;
    
    [SerializeField] private float NormalSpeed;
    [SerializeField] private float SprintSpeed;
    [SerializeField] private float JumpForce;
    [SerializeField] public float CameraSensitivity;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float Gravity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        Physics.gravity = new Vector3(0, -Gravity, 0);
        
        Speed = NormalSpeed;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        
        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = SprintSpeed;
        }
        else
        {
            Speed = NormalSpeed;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Debug.Log("Jump");
            rb.velocity = new Vector3(rb.velocity.x, JumpForce, rb.velocity.z);
        }

        float mouseX = Input.GetAxis("Mouse X") * CameraSensitivity;
        transform.Rotate(0, mouseX, 0);
        
        Vector3 movement = (transform.right * inputX + transform.forward * inputZ) * Speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, bc.bounds.extents.y + 0.1f, GroundLayer);
    }
}