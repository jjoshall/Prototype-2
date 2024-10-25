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

    public bool canRotate = true; // Control whether player can rotate with the mouse

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerCamera.localRotation = Quaternion.Euler(pitchRotation, 0f, 0f);

    }

    void Update()
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
        rb.MovePosition(rb.position + movement * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
