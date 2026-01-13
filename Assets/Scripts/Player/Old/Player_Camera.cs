using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    [SerializeField] private float mouseSensitivity = 3f;
    private float xRotation;

    void Awake()
    {
        var mainCam = Camera.main;
        if (mainCam)
        {
            mainCam.enabled = false;
            mainCam.GetComponent<AudioListener>().enabled = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
