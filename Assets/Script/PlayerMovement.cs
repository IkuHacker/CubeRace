using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float continuousSpeed = 5f;
    public float jumpForce = 10f;
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;
    public float maxJumpTime = 0.5f;
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

    // Reference to the TrailRenderer component
    public TrailRenderer trailRenderer;
    public ParticleSystem particleSystemPlayer;

    // Reference to the current material of the player
    public Material currentMaterial;
    public float darkFactor;

    public Animator playerAnimator;


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
        particleSystemPlayer.Play();

        rb = GetComponent<Rigidbody>();

        // Initialize the TrailRenderer and currentMaterial references
        if (trailRenderer == null)
        {
            trailRenderer = GetComponent<TrailRenderer>();
        }
        if (currentMaterial == null)
        {
            currentMaterial = GetComponent<Renderer>().material;
        }

        // Set the initial trail color to the opposite color of the current material
    }

    private void Update()
    {
        currentMaterial = GetComponent<Renderer>().material;
        UpdateTrailColor();

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

        if (isJumping) 
        {
            playerAnimator.SetBool("isJumping", true);
            Debug.Log("Stretch");
        }
        else 
        {
            playerAnimator.SetBool("isJumping", false);

        }
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
            playerAnimator.SetBool("isOnGround", true);
            isGrounded = true;
            isJumping = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerAnimator.SetBool("isOnGround", false);
            isGrounded = false;
        }
    }

    // Method to calculate and set the opposite color
    private void UpdateTrailColor()
    {
        if (currentMaterial != null && trailRenderer != null)
        {
            Color currentColor = currentMaterial.color;
            Color oppositeColor = new Color(1f - currentColor.r, 1f - currentColor.g, 1f - currentColor.b);

            // Réduire l'intensité pour obtenir une couleur plus foncée
            Color darkerOppositeColor = new Color(oppositeColor.r * darkFactor, oppositeColor.g * darkFactor, oppositeColor.b * darkFactor);

            trailRenderer.startColor = oppositeColor;
            trailRenderer.endColor = oppositeColor;

            var mainModule = particleSystemPlayer.main;
            mainModule.startColor = darkerOppositeColor;
        }
    }
}
