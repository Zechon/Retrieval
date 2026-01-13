using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField] float sensX;
    [SerializeField] float sensY;

    [Header("References")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform cameraPivot;
    [SerializeField] PlayerInputHandler input;

    private float pitch;

    private void Start()
    {
        CursorLocker.Lock();
    }

    private void OLD_LateUpdate()
    {
        //read mouse input
        Vector2 look = input.Look;
    
        float yaw = look.x * sensX;
        float pitchDelta = look.y * sensY;
    
        pitch -= pitchDelta;
        pitch = Mathf.Clamp(pitch, -89f, 89f);
    
        //Apply rotations
        orientation.Rotate(Vector3.up * yaw);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * sensY;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -85f, 85f);

        cameraPivot.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        orientation.Rotate(Vector3.up * mouseX);
    }
}
