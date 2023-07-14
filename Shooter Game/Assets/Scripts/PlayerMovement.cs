using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

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
        grounded = Physics.Raycast(base.transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
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
            Invoke("ResetJump", jumpCooldown);
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
        AudioClip clip = jumpSounds[num];
        playerLegs.PlayOneShot(clip);
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(base.transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
