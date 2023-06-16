using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; // The object that the camera should follow and rotate around
    [SerializeField, Range(0f, 20f)] private float followDistance = 10f; // The distance at which the camera should follow the target
    [SerializeField, Range(0f, 50f)] private float rotationSpeed = 10f; // The speed at which the camera should rotate around the target
    [SerializeField, Range(0f, 1f)] private float minMovementThreshold = 0.1f; // The minimum movement threshold required for the camera to rotate

    private Vector3 previousPosition; // The target's position in the previous frame
    private Quaternion targetRotation; // The rotation of the camera around the target

    private void Start()
    {
        // Initialize the previous position to the target's starting position
        previousPosition = target.position;

        // Initialize the target rotation to the current rotation of the camera
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        // Calculate the position that the camera should be at, based on the target's position and the follow distance
        Vector3 targetPosition = target.position + (target.forward * followDistance);

        // Lerp the camera's position towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        // Calculate the difference between the target's current position and its previous position
        Vector3 movementDelta = target.position - previousPosition;

        // If the target has moved more than the minimum movement threshold, rotate the camera around the target's y-axis
        if (movementDelta.magnitude > minMovementThreshold)
        {
            // Calculate the new target rotation based on the movement delta
            targetRotation *= Quaternion.FromToRotation(previousPosition - target.position, movementDelta);

            // Remove the rotation around the x and z axes from the target rotation
            targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
        }

        // Lerp the camera's rotation towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);

        // Update the previous position to the current position
        previousPosition = target.position;
    }
}
