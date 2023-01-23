using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider bc;

    private float dirX = 0f;
    private float dirZ = 0f;
    [SerializeField] private float speed = 0f;

    [SerializeField] private float jumpForce = 0f;
    [SerializeField] public float CameraSensitivity = 0f;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        
        Vector3 movement = (transform.right * inputX + transform.forward * inputZ) * speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        // rotate player on the y axis with mouse
        float mouseX = Input.GetAxis("Mouse X") * CameraSensitivity;
        transform.Rotate(0,mouseX,0);        
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, bc.bounds.extents.y + 0.1f, groundLayer);
    }
}