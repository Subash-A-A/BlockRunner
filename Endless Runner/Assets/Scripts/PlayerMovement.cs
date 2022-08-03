using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] float topSpeed = 100f;
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float sideSpeed = 10f;
    [SerializeField] float jumpForce = 30f;
    [SerializeField] int extraJumpCount = 3;
    [SerializeField] float armTiltMultiplier = 5f;
    [SerializeField] float XPosClamp = 8f;

    [Header("References")]
    [SerializeField] Transform GroundCheckTransform;
    [SerializeField] Transform LWallCheckTransform;
    [SerializeField] Transform RWallCheckTransform;
    [SerializeField] Transform ArmHolder;
    [SerializeField] LayerMask WhatIsGround;
    [SerializeField] Animator anim;

    private Rigidbody rb;
    
    private int jumpCount = 0;
    private float armTilt = 0f;
    private float animRunSpeed = 0f;
    private float gravityConstant = 9.14f;
    private float gravityScale = 1f;

    // Detection Variables
    public bool isGrounded;
    private bool wallR = false;
    private bool wallL = false;

    // Input Variables
    private float horizontal;
    private bool isJumping;

    private void Start()
    {   
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        rb.useGravity = false;
    }

    private void Update()
    {
        PlayerAnimations();
        MyInput();
        Jump();
        GroundCheck();
        PlayerArmLean();
    }

    private void FixedUpdate()
    {
        Move();
        Gravity();
        XPosClamper();
    }

    void Move()
    {
        // Forward force
        rb.AddForce(Vector3.forward * playerSpeed);

        // Side Movement Clamping to top speed
        rb.velocity = new Vector3(sideSpeed * horizontal, rb.velocity.y, Mathf.Clamp(rb.velocity.z, 0f, topSpeed));
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(GroundCheckTransform.position, 0.4f, WhatIsGround);
        wallR = Physics.CheckSphere(RWallCheckTransform.position, 0.4f, WhatIsGround);
        wallL = Physics.CheckSphere(LWallCheckTransform.position, 0.4f, WhatIsGround);
    }

    void PlayerAnimations()
    {
        // In Air
        anim.SetBool("isGrounded", isGrounded);
        
        // Walk Run Blend
        animRunSpeed = rb.velocity.z / 100f;
        animRunSpeed = Mathf.Clamp(animRunSpeed, 0f, 1f);
        anim.SetFloat("velZ", animRunSpeed);
    }

    void Jump()
    {
        if (isJumping && jumpCount < extraJumpCount)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

    void PlayerArmLean()
    {   
        armTilt = Mathf.Lerp(armTilt, isGrounded?horizontal*3:horizontal, 10 * Time.deltaTime);
        ArmHolder.localRotation = Quaternion.Euler(0f, 0f, armTilt * armTiltMultiplier * -1f);
    }

    void MyInput()
    {
        isJumping = Input.GetKeyDown(KeyCode.Space);
        horizontal = Input.GetAxis("Horizontal");
    }

    void Gravity()
    {
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravityConstant * gravityScale, ForceMode.Force);
        }
    }

    void XPosClamper()
    {
        // Project where our velocity will take us by the end of the frame.
        Vector3 positionAtEndOfStep = rb.position + rb.velocity * Time.deltaTime;

        // Limit that projected position to within our allowed bounds.
        positionAtEndOfStep.x = Mathf.Clamp(positionAtEndOfStep.x, -XPosClamp, XPosClamp);

        // Compute a velocity that will take us to this clamped position instead.
        Vector3 neededVelocity = (positionAtEndOfStep - rb.position) / Time.deltaTime;
        rb.velocity = neededVelocity;
    }
}
