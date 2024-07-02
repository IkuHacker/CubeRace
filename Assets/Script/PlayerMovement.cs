using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float continuousSpeed = 5f;
    public float jumpForce = 10f;
    public float coyoteTime = 0.2f; // Time window for coyote time
    public float jumpBufferTime = 0.2f; // Time window for jump buffer
    public float maxJumpTime = 0.5f; // Maximum time the player can hold the jump button to go higher
    public bool isGrounded;

    private float horizontalMovement;
    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private float jumpTimeCounter;
    private bool isJumping;
    private bool isHoldingJump;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scène");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * speed;

        // Coyote time counter update
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Jump buffer counter update
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Check if we can jump or continue jumping
        if ((jumpBufferCounter > 0f && coyoteTimeCounter > 0f) || (isJumping && jumpTimeCounter > 0f))
        {
            if (!isJumping)
            {
                isJumping = true;
                isHoldingJump = true;
                jumpTimeCounter = maxJumpTime;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (isHoldingJump && Input.GetButton("Jump"))
            {
                if (jumpTimeCounter > 0f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isHoldingJump = false;
                }
            }
        }

        // Stop jumping when the jump button is released
        if (Input.GetButtonUp("Jump"))
        {
            isHoldingJump = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer(horizontalMovement);
    }

    private void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector3(_horizontalMovement, rb.velocity.y, continuousSpeed);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.05f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false; // Reset jumping state when hitting the ground
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
