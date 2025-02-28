using UnityEngine;

public class MovePlayer : MonoBehaviour
{
  public float moveSpeed = 5f;
  public float jumpHeight = 2f;
  public float gravity = 9.8f;

  private CharacterController controller;
  private Vector3 velocity;
  private bool isGrounded;

  public Transform cameraTransform;
  public float mouseSensitivity = 2f;
  private float verticalRotation = 0f;

  void Start()
  {
    controller = GetComponent<CharacterController>();
    Cursor.lockState = CursorLockMode.Locked;
  }

  void Update()
  {
    // Check if the player is grounded
    isGrounded = controller.isGrounded;
    if (isGrounded && velocity.y < 0)
    {
      velocity.y = -2f;
    }

    // Player Movement
    float moveX = Input.GetAxis("Horizontal");
    float moveZ = Input.GetAxis("Vertical");

    Vector3 move = transform.right * moveX + transform.forward * moveZ;
    controller.Move(move * moveSpeed * Time.deltaTime);

    // Jumping
    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
      velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
    }

    // Apply Gravity
    velocity.y -= gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);

    // Mouse Look
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

    transform.Rotate(Vector3.up * mouseX);

    verticalRotation -= mouseY;
    verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
    cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

    // Toggle collision when holding SHIFT
    if (Input.GetKey(KeyCode.LeftShift))
    {
      controller.enabled = false;  // Pass through objects
    }
    else
    {
      controller.enabled = true;   // Enable normal collision
    }
  }
}
