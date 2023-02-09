using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PauseMenu pauseMenu;
    
    private Rigidbody rb;
    private CapsuleCollider cc;
    private float Speed;
    private Animator anim;
    public bool NotRotating = true;
    private float mouseX;
    private bool GameIsPaused = false;

    [SerializeField] private float NormalSpeed;
    [SerializeField] private float SprintSpeed;
    [SerializeField] private float JumpForce;
    [SerializeField] public float CameraSensitivity;
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private float Gravity;
    [SerializeField] private float AnimationSpeed;
    [SerializeField] private GameObject pauseCanvas;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, -Gravity, 0);
        Speed = NormalSpeed;
        Cursor.visible = false;
        pauseMenu = pauseCanvas.GetComponent<PauseMenu>();
    }

    void Update()
    {
        GameIsPaused = pauseMenu.GameIsPaused;
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
        
        mouseX = Input.GetAxis("Mouse X") * CameraSensitivity;
        if (GameIsPaused == false)
        {
            transform.Rotate(0, mouseX, 0);
        }

        Vector3 movement = (transform.right * H_Input + transform.forward * V_Input) * Speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        AnimationSpeed = Speed;
        anim.speed = AnimationSpeed;
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