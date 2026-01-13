using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Player_Inputs inputs;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }

    public bool JumpPressed { get; private set; }
    public bool JumpHeld { get; private set; }

    private void Awake()
    {
        inputs = new Player_Inputs();
    }

    private void OnEnable()
    {
        inputs.Enable();
        RegisterCallbacks();
    }
    
    private void OnDisable()
    {
        UnregisterCallbacks();
        inputs.Disable();
    }

    private void Update()
    {
        Look = inputs.Player.Look.ReadValue<Vector2>();
    }

    private void RegisterCallbacks()
    {
        inputs.Player.Move.performed += OnMove;
        inputs.Player.Move.canceled += OnMove;

        inputs.Player.Jump.performed += OnJumpPerformed;
        inputs.Player.Jump.canceled += OnJumpCanceled;
    }

    private void UnregisterCallbacks()
    {
        inputs.Player.Move.performed -= OnMove;
        inputs.Player.Move.canceled -= OnMove;

        inputs.Player.Jump.performed -= OnJumpPerformed;
        inputs.Player.Jump.canceled -= OnJumpCanceled;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        Move = ctx.ReadValue<Vector2>();
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        JumpPressed = true;
        JumpHeld = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext ctx)
    {
        JumpHeld = false;
    }

    private void LateUpdate()
    {
        JumpPressed = false;
    }
}
