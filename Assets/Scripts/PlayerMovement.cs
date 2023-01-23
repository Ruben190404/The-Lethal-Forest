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
    // [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(dirX * speed, rb.velocity.y, rb.velocity.z);
        dirZ = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, dirZ * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        // rotate player on the y axis with mouse
        float mouseX = Input.GetAxis("Mouse X") * CameraSensitivity;
        transform.Rotate(0,0,mouseX);        
    }
}