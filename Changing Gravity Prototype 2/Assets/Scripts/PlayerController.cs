using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private float yawRotation = 0f;
    private float pitchRotation = 0f;
    private bool isGrounded;
    public bool canRotate = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCamera.localRotation = Quaternion.Euler(pitchRotation, 0f, 0f);
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        HandleMovement();
        HandleJump();

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
        rb.MovePosition(rb.position + movement * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector3 jumpDirection = -Physics.gravity.normalized;
            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
        }
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
