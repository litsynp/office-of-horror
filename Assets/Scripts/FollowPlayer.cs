using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
  public Transform target;  // Assign player transform
  public float sensitivity = 2f;
  private float verticalRotation = 0f;

  void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
  }

  void Update()
  {
    // Get mouse movement
    float mouseX = Input.GetAxis("Mouse X") * sensitivity;
    float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

    // Rotate the player (left-right)
    target.Rotate(Vector3.up * mouseX);

    // Rotate the camera (up-down)
    verticalRotation -= mouseY;
    verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
    transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
  }
}