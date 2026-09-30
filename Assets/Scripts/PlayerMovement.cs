using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    //Gerneral movement variables
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float jumpCutMultiplier = 0.5f;
    private bool normalJumpInProgress = false;
    //GroundCHeck variables
    [SerializeField] private LayerMask groundLayer;
    private float groundCheckDistance = 0.05f;
    //Coyote Timer variables
    [SerializeField] private float maxCoyoteTime = 0.1f;
    private float currentCoyoteTimer;
    //Jump Buffer variables
    [SerializeField] private float maxJumpBufferTime =0.12f;
    private float currentJumpBufferTimer;
    private PlayerAudio playerAudio;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerAudio = GetComponent<PlayerAudio>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            currentJumpBufferTimer = maxJumpBufferTime;

        }
        else if(normalJumpInProgress && rb.linearVelocity.y > 0)
        {
            Vector2 velocity = rb.linearVelocity;
            velocity.y *= jumpCutMultiplier;
            rb.linearVelocity = velocity;

            normalJumpInProgress = false;

        }
    }

    private void Update()
    {
        bool grounded = IsGrounded();
        currentJumpBufferTimer -= Time.deltaTime;
        if (grounded)
        {
            currentCoyoteTimer = maxCoyoteTime;
        }
        else
        {
            currentCoyoteTimer -= Time.deltaTime;
        }
        if (currentCoyoteTimer > 0 && currentJumpBufferTimer > 0)
        {
            currentCoyoteTimer = 0;
            currentJumpBufferTimer = 0;

            normalJumpInProgress = true;

            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpSpeed
            );

            playerAudio.PlayJump();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            moveInput.x * moveSpeed,
            rb.linearVelocity.y
        );
    }

    private bool IsGrounded()
    {
        Vector2 boxSize = new Vector2(
            boxCollider.bounds.size.x * 0.9f,
            boxCollider.bounds.size.y
        );

        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxSize,
            0f,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );
        return hit.collider != null;
        
    }
}
