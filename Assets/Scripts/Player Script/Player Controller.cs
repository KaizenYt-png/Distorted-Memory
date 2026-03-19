using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStat playerParameters;

    //Jumping
    bool readyToJump;

    //Crouching
    private float startYScale;

    //Ground Check
    bool grounded;

    //Slope Handling
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerParameters.playerHeight * 0.5f + 0.2f, playerParameters.whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();



        // handle drag
        if (grounded)
            rb.linearDamping = playerParameters.groundDrag;
        else
            rb.linearDamping = 0;
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
        if (Input.GetKey(playerParameters.jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), playerParameters.jumpCooldown);
        }

        // start crouch
        if (Input.GetKeyDown(playerParameters.crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, playerParameters.crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop crouch
        if (Input.GetKeyUp(playerParameters.crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        // Mode - Crouching
        if (Input.GetKey(playerParameters.crouchKey))
        {
            state = MovementState.crouching;
            playerParameters.moveSpeed = playerParameters.crouchSpeed;
        }

        // Mode - Sprinting
        else if (grounded && Input.GetKey(playerParameters.sprintKey))
        {
            state = MovementState.sprinting;
            playerParameters.moveSpeed = playerParameters.sprintSpeed;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            playerParameters.moveSpeed = playerParameters.walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * playerParameters.moveSpeed * 20f, ForceMode.Force);

            if (rb.linearVelocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if (grounded)
            rb.AddForce(moveDirection.normalized * playerParameters.moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * playerParameters.moveSpeed * 10f * playerParameters.airMultiplier, ForceMode.Force);

        // turn gravity off while on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.linearVelocity.magnitude > playerParameters.moveSpeed)
                rb.linearVelocity = rb.linearVelocity.normalized * playerParameters.moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > playerParameters.moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * playerParameters.moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * playerParameters.jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerParameters.playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < playerParameters.maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }


    

    
}
