using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // Drag TestigoCharacter here
    public float smoothSpeed = 5f; // Smooth camera movement
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    void LateUpdate()
    {
        if (target == null) return;

        // Follow player smoothly so only the current gallery section is visible
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
