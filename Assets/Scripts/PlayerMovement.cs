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
    public bool NotRotating = true;
    private float mouseX;

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
        Cursor.visible = false;
    }

    void Update()
    {
        UpdateAnimation();
        Debug.Log(mouseX);
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
        
        mouseX = Input.GetAxis("Mouse X") * CameraSensitivity;
        transform.Rotate(0, mouseX, 0);
        
        Vector3 movement = (transform.right * H_Input + transform.forward * V_Input) * Speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        AnimationSpeed = Speed;
        
    }

    void UpdateAnimation()
    {
        if (rb.velocity.magnitude > 0.1)
        {
            anim.SetInteger("State", 2);
        }
        else if (mouseX < -1 && NotRotating)
        {
            anim.SetInteger("State", 3);
            NotRotating = false;
        }
        else if (mouseX > 1 && NotRotating)
        {
            anim.SetInteger("State", 4);
            NotRotating = false;
        }
        else
        {
            anim.speed = 1;
            anim.SetInteger("State", 1);
            NotRotating = true;
        }
    }

    void StopRotate()
    {
        NotRotating = true;
    }
}