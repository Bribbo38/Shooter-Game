using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    /*public float moveSpeed;

    public float groundDrag;

    public float jumpForce;

    public float jumpCooldown;

    public float airMultiplier;

    private bool readyToJump = true;

    public float playerHeight;

    public LayerMask whatIsGround;

    public bool grounded;

    public Transform orientation;

    private float horizontalInput;

    private float verticalInput;

    public KeyCode jumpKey = KeyCode.Space;

    public AudioClip[] jumpSounds;

    public AudioSource playerLegs;

    private Vector3 moveDirection;

    private Rigidbody rb;

    public float killHeight = -10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(base.transform.position, Vector3.down, playerHeight, whatIsGround);
        //Debug.DrawLine(base.transform.position, base.transform.position + Vector3.down * playerHeight, grounded ? Color.green : Color.red, .5f);
        MyInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0f;
        }
        if (base.transform.position.y < killHeight)
        {
            MonoBehaviour.print("dead");
        }
    }

    private void FixedUpdate()
    {
        if (!PauseMenu.paused)
        {
            MovePlayer();
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 vector = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (vector.magnitude > moveSpeed)
        {
            Vector3 vector2 = vector.normalized * moveSpeed;
            rb.velocity = new Vector3(vector2.x, rb.velocity.y, vector2.z);
        }
    }

    private void Jump()
    {
        int num = Random.Range(0, jumpSounds.Length);
        //AudioClip clip = jumpSounds[num];
        //playerLegs.PlayOneShot(clip);
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(base.transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    //
    [SerializeField]
    private float speed = 6.0f;
    [SerializeField]
    private float jumpHeight = 2.0f;
    [SerializeField]
    private float gravity = 20.0f;
    [SerializeField]
    private Transform cameraTransform;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = (cameraTransform.right * horizontal + cameraTransform.forward * vertical).normalized;
        movement.y = 0f;

        controller.Move(movement * speed * Time.deltaTime);

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
        }

        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }
    */

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float groundDrag;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpCooldown;
    [SerializeField]
    private  float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
