using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider cc;
    private float Speed;
    private Animator anim;

    [SerializeField] private float NormalSpeed;
    [SerializeField] private float SprintSpeed;
    [SerializeField] private float JumpForce;
    [SerializeField] public float CameraSensitivity;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float Gravity;
    [SerializeField] private float AnimationSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, -Gravity, 0);
        
        Speed = NormalSpeed;
    }

    void Update()
    {
        Debug.Log(anim.speed);

        UpdateAnimation();
        
        float H_Input = Input.GetAxisRaw("Horizontal");
        float V_Input = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = SprintSpeed;
        }
        else
        {
            Speed = NormalSpeed;
        }
        
        float mouseX = Input.GetAxis("Mouse X") * CameraSensitivity;
        transform.Rotate(0, mouseX, 0);
        
        Vector3 movement = (transform.right * H_Input + transform.forward * V_Input) * Speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        AnimationSpeed = Speed;
        
    }

    void UpdateAnimation()
    {
        if (rb.velocity.magnitude > 0.1)
        {
            anim.speed = AnimationSpeed;
            anim.ResetTrigger("Idle");
            anim.ResetTrigger("TurnRight");
            anim.ResetTrigger("TurnLeft");
            anim.SetTrigger("Crawl");
        }
        // else if (rb.rotation.y < 0.1)
        // {
        //     anim.ResetTrigger("Idle");
        //     anim.ResetTrigger("TurnRight");
        //     anim.ResetTrigger("Crawl");
        //     anim.SetTrigger("TurnLeft");
        // }
        // else if (rb.rotation.y > 0.1)
        // {
        //     anim.ResetTrigger("Idle");
        //     anim.ResetTrigger("TurnLeft");
        //     anim.ResetTrigger("Crawl");
        //     anim.SetTrigger("TurnRight");
        // }
        else
        {
            anim.speed = 1;
            anim.ResetTrigger("Crawl");
            anim.ResetTrigger("Idle");
            anim.ResetTrigger("TurnRight");
            anim.ResetTrigger("TurnLeft");
            anim.SetTrigger("Idle");
        }
    }
}