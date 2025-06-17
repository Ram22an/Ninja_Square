using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // Reference to the player's transform
    public Vector3 offset;         // Offset between camera and player
    public float smoothSpeed = 0.125f;  // Smoothing factor

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
