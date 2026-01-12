using UnityEngine;
public class PlayerCameraDriver : MonoBehaviour
{
    public PlayerInputHandler input;
    public PlayerCamera camController;

    private void Start()
    {
        CursorLocker.Lock();
    }

    private void LateUpdate()
    {
        camController.Look(input.Look);
    }
}