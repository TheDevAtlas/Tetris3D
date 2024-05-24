
using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float joystickSensitivity = 100f;
    public float verticalAngleMin = -90f;
    public float verticalAngleMax = 90f;

    private float xAxisRotation = 0f;

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        WiiU.GamePadState state = WiiU.GamePad.access.state;

        // Mouse Input
        float mouseX = (Input.GetAxis("Mouse X") + state.rStick.x) * mouseSensitivity * Time.deltaTime;
        float mouseY = (Input.GetAxis("Mouse Y") + state.rStick.y) * mouseSensitivity * Time.deltaTime;

        // PS5 Controller Joystick Input (Right Stick)
        // float joystickX = Input.GetAxis("Right Stick Horizontal") * joystickSensitivity * Time.deltaTime;
        // float joystickY = Input.GetAxis("Right Stick Vertical") * joystickSensitivity * Time.deltaTime;

        // Combine mouse and joystick input
        // mouseX += joystickX;
        // mouseY += joystickY;

        // Calculate rotation around the x-axis (up and down)
        xAxisRotation -= mouseY;
        xAxisRotation = Mathf.Clamp(xAxisRotation, verticalAngleMin, verticalAngleMax);

        // Apply rotation around the x-axis
        transform.localEulerAngles = new Vector3(xAxisRotation, transform.localEulerAngles.y, 0f);

        // Rotate around the y-axis (left and right)
        transform.Rotate(Vector3.up * mouseX);
    }
}
