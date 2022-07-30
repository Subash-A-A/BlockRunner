using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float sideSpeed = 10f;
    [SerializeField] float jumpForce = 300f;
    [SerializeField] Transform GroundCheckTransform;
    [SerializeField] LayerMask WhatIsGround;
    [SerializeField] Animator anim;

    private Rigidbody rb;
    private bool isGrounded;

    // Input Variables
    private float horizontal;
    private bool isJumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        isGrounded = true;
    }

    private void Update()
    {
        PlayerAnimations();
        MyInput();
    }

    private void FixedUpdate()
    {
        Move();
        GroundCheck();
        Jump();
    }

    void Move()
    {
        // Forward force
        rb.AddForce(Vector3.forward * playerSpeed);

        rb.velocity = new Vector3(horizontal * sideSpeed, rb.velocity.y, rb.velocity.z);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(GroundCheckTransform.position, 0.4f, WhatIsGround);
    }

    void PlayerAnimations()
    {
        // In Air
        anim.SetBool("isGrounded", isGrounded);
    }

    void Jump()
    {
        if (isJumping && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    void MyInput()
    {
        isJumping = Input.GetKeyDown(KeyCode.Space);
        horizontal = Input.GetAxis("Horizontal");
    }
}
