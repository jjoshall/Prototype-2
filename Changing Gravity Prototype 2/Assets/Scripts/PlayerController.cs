using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;

    private Rigidbody rb;
    private float yawRotation = 0f;
    private float pitchRotation = 0f;

    bool isGrounded;
    private bool jumpRequested = false;

    public bool canRotate = true; // Control whether player can rotate with the mouse

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerCamera.localRotation = Quaternion.Euler(pitchRotation, 0f, 0f);

    }

    private void Update()
    {
          if (Input.GetButtonDown("Jump"))
          {
               jumpRequested = true;
          }
     }

     void FixedUpdate()
    {
        HandleMovement();
        HandleJump();

        // Only rotate if canRotate is true
        if (canRotate)
        {
            HandleMouseRotation();
            HandleCameraPitch();
        }
    }

     private void HandleMovement()
     {        
          float moveX = Input.GetAxis("Horizontal");
          float moveZ = Input.GetAxis("Vertical");

          Vector3 movement = (transform.right * moveX + transform.forward * moveZ).normalized * moveSpeed;

          Vector3 velocity = rb.velocity;

          velocity.x = movement.x;
          velocity.z = movement.z;

          rb.velocity = velocity;
     }

    private void HandleJump()
    {
        // check if player is grounded
        isGrounded = Physics.Raycast(transform.position, -transform.up, 1.1f);

        if (jumpRequested && isGrounded)
        {
            Debug.Log("Jumping");
            Vector3 jumpDirection = -Physics.gravity.normalized;
            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
            Debug.Log($"Jump Force: {jumpForce}, Gravity Magnitude: {Physics.gravity.magnitude}, Jump Direction: {jumpDirection}");
        }

        jumpRequested = false;
     }

    private void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        yawRotation += mouseX;

        Quaternion yawQuaternion = Quaternion.AngleAxis(yawRotation, -Physics.gravity.normalized);
        transform.rotation = yawQuaternion * Quaternion.FromToRotation(Vector3.up, -Physics.gravity.normalized);
    }

    private void HandleCameraPitch()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitchRotation -= mouseY;
        pitchRotation = Mathf.Clamp(pitchRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(pitchRotation, 0f, 0f);
    }
}
