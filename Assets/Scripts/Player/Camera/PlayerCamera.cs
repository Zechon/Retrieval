using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField] float sensX;
    [SerializeField] float sensY;

    [Header("References")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform cameraPivot;
    [SerializeField] private PauseMenuHandler pauseHandler;

    private float pitch;
    Vector2 updateLook;
    private void Start()
    {
       // pauseHandler = GameObject.FindGameObjectWithTag("Pause").GetComponent<PauseMenuHandler>();
        CursorLocker.Lock();
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
