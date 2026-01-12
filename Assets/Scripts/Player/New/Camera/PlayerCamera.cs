using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("References")]
    public Transform playerRoot;
    public Transform cameraPivot;

    [Header("Settings")]
    public float sensitivityX = 0.6f;
    public float sensitivityY = 0.4f;
    public float minPitch = -89f;
    public float maxPitch = 89f;

    private float pitch;

    public void Look(Vector2 lookInput)
    {
        float yaw = lookInput.x * sensitivityX;
        float pitchDelta = lookInput.y * sensitivityY;

        pitch -= pitchDelta;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        playerRoot.Rotate(Vector3.up * yaw);
    }
}
