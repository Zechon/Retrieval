using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float gravity = -25f;
    public float jumpForce = 1.6f;

    [Header("References")]
    public Transform orientation;
    public PlayerInputHandler input;

    private CharacterController controller;
    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleGravity();
    }

    private void HandleMovement()
    {
        Vector2 moveInput = input.Move;

        Vector3 moveDir =
            orientation.forward * moveInput.y +
            orientation.right * moveInput.x;

        controller.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    private void HandleGravity()
    {
        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        if (controller.isGrounded && input.JumpPressed)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

