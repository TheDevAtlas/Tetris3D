using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDelayMove : MonoBehaviour
{
    public Vector3 targetRotationEulerAngles;
    public float targetOrthographicSize = 5f; // Target size for the orthographic camera
    public float delayTime = 1.0f; // Time before changes start
    public float transitionTime = 2.0f; // Duration of the changes

    private Quaternion targetRotation;
    private float initialOrthographicSize;
    private bool isTransitioning = false;

    void Start()
    {
        // Ensure this script is attached to an orthographic camera
        //if (!Camera.main.orthographic)
        //{
        //    Debug.LogError("SmoothCameraRotationAndZoom requires an orthographic camera.");
         //   return;
        //}

        // Convert the Euler angles to a Quaternion and store the initial size
        targetRotation = Quaternion.Euler(targetRotationEulerAngles);
        initialOrthographicSize = Camera.main.orthographicSize;

        // Start the transition process
        StartCoroutine(TransitionCameraProperties());
    }

    IEnumerator TransitionCameraProperties()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        // Start transitions
        isTransitioning = true;
        float elapsedTime = 0f;

        // Store the initial rotation
        Quaternion initialRotation = transform.rotation;

        while (elapsedTime < transitionTime)
        {
            // Calculate the transition progress
            float progress = elapsedTime / transitionTime;
            float smoothedProgress = Mathf.SmoothStep(0.0f, 1.0f, progress);

            // Smoothly interpolate between the initial rotation and the target rotation
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, smoothedProgress);

            // Smoothly interpolate the orthographic size
            Camera.main.orthographicSize = Mathf.Lerp(initialOrthographicSize, targetOrthographicSize, smoothedProgress);

            // Increase the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait until next frame
            yield return null;
        }

        // Ensure the final rotation and size are exactly the target values
        transform.rotation = targetRotation;
        Camera.main.orthographicSize = targetOrthographicSize;
        isTransitioning = false;
    }
}
