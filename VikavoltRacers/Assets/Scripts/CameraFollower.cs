using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // the object to follow

    [Header("Follow Settings")]
    public Vector3 offset = new Vector3(0, 5, -10); // camera offset from target
    public float smoothSpeed = 5f; // how fast the camera follows

    void LateUpdate()
    {
        if (target == null) return;

        // Desired position with offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move camera towards desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Make camera look at the target
        transform.LookAt(target);
    }
}

