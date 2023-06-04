using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 3.0f; // Speed of camera rotation

    private float mouseX; // Mouse X position
    private float mouseY; // Mouse Y position

    public float maxY;
    public float minY;

    private void Start()
    {
        // Disable cursor visibility and lock it to the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Get mouse movement
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, minY, maxY); // Clamp vertical rotation to avoid flipping

        // Rotate the camera based on mouse movement
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        transform.rotation = rotation;

        // Check if the escape key is pressed to unlock the cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
