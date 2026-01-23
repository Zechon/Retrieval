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
    [SerializeField] private PauseMenuHandler pauseHandler;

    private float pitch;
    Vector2 updateLook;
    private void Start()
    {
       // pauseHandler = GameObject.FindGameObjectWithTag("Pause").GetComponent<PauseMenuHandler>();
        CursorLocker.Lock();
    }
    private void Update()
    {
        updateLook = input.Look * Time.deltaTime;
    }

    //TODO: Find out why Input System does not work here
    private void NEW_LateUpdate()
    {
        //read mouse input
        Vector2 look = updateLook;// input.Look;
    
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
        if (pauseHandler != null && pauseHandler.paused) return;

        float mouseX = Input.GetAxis("Mouse X") * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * sensY;

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -85f, 85f);

        cameraPivot.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        orientation.Rotate(Vector3.up * mouseX);
    }
}
