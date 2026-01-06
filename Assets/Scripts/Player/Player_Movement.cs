using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravityMultiplier = 2f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask thisIsGround;
    [SerializeField] private float groundCheckOffset = 0.1f;

    [Header("Other Setup")]
    private PlayerInputActions inputActions;
    private Vector2 moveInput;
    private Rigidbody rb;

    [Header("Tracking Variables")]
    private bool isGrounded;
    private float playerHeight;
    private float verticalVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new PlayerInputActions();

        CapsuleCollider capsule = GetComponent<CapsuleCollider>();

        if (capsule != null)
            playerHeight = capsule.height * transform.localScale.y;
        else
            Debug.LogWarning("No collider found! Please attach a Capsule Collider.");
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;
    }

    private void OnDisable() => inputActions.Disable();

    private void FixedUpdate()
    {

        CheckGrounded();

        Move();
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + groundCheckOffset, thisIsGround);
    }

    private void Move()
    {
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        Vector3 delta = transform.TransformDirection(moveDir) * moveSpeed;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = delta.x;
        velocity.z = delta.z;
        rb.linearVelocity = velocity;
    }
}