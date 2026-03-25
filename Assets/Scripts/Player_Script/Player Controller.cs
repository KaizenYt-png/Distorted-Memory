using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStat playerParameters;

    //Jumping
    bool _readyToJump;

    //Crouching
    private float _startYScale;

    //Ground Check
    bool _grounded;

    //Slope Handling
    private RaycastHit _slopeHit;
    private bool _exitingSlope;

    public Transform orientation;

    float _horizontalInput;
    float _verticalInput;

    Vector3 _moveDirection;

    Rigidbody _rb;

    public MovementState state;
    public enum MovementState
    {
        Walking,
        Sprinting,
        Crouching,
        Air
    }

    private void Start()
    {
        
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        _readyToJump = true;

        _startYScale = transform.localScale.y;
    }

    private void Update()
    {
        // ground check
        _grounded = Physics.Raycast(transform.position, Vector3.down, playerParameters.playerHeight * 0.5f + 0.2f, playerParameters.whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();



        // handle drag
        if (_grounded)
            _rb.linearDamping = playerParameters.groundDrag;
        else
            _rb.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(playerParameters.jumpKey) && _readyToJump && _grounded)
        {
            _readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), playerParameters.jumpCooldown);
        }

        // start crouch
        if (Input.GetKeyDown(playerParameters.crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, playerParameters.crouchYScale, transform.localScale.z);
            _rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop crouch
        if (Input.GetKeyUp(playerParameters.crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, _startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        // Mode - Crouching
        if (Input.GetKey(playerParameters.crouchKey))
        {
            state = MovementState.Crouching;
            playerParameters.moveSpeed = playerParameters.crouchSpeed;
        }

        // Mode - Sprinting
        else if (_grounded && Input.GetKey(playerParameters.sprintKey))
        {
            state = MovementState.Sprinting;
            playerParameters.moveSpeed = playerParameters.sprintSpeed;
        }

        // Mode - Walking
        else if (_grounded)
        {
            state = MovementState.Walking;
            playerParameters.moveSpeed = playerParameters.walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.Air;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

        // on slope
        if (OnSlope() && !_exitingSlope)
        {
            _rb.AddForce(GetSlopeMoveDirection() * playerParameters.moveSpeed * 20f, ForceMode.Force);

            if (_rb.linearVelocity.y > 0)
                _rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if (_grounded)
            _rb.AddForce(_moveDirection.normalized * playerParameters.moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!_grounded)
            _rb.AddForce(_moveDirection.normalized * playerParameters.moveSpeed * 10f * playerParameters.airMultiplier, ForceMode.Force);

        // turn gravity off while on slope
        _rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !_exitingSlope)
        {
            if (_rb.linearVelocity.magnitude > playerParameters.moveSpeed)
                _rb.linearVelocity = _rb.linearVelocity.normalized * playerParameters.moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > playerParameters.moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * playerParameters.moveSpeed;
                _rb.linearVelocity = new Vector3(limitedVel.x, _rb.linearVelocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        _exitingSlope = true;

        // reset y velocity
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);

        _rb.AddForce(transform.up * playerParameters.jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        _readyToJump = true;

        _exitingSlope = false;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, playerParameters.playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < playerParameters.maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal).normalized;
    }


    

    
}
