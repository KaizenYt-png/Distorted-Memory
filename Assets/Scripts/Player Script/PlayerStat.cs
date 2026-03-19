using UnityEngine;

[CreateAssetMenu(fileName = "Player Parameter", menuName = "Player Parameter")]
public class PlayerStat : ScriptableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    public LayerMask whatIsGround;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Slope Handling")]
    public float maxSlopeAngle;

    [Header("Ground Check")]
    public float playerHeight;
}
