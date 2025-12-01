using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float sensitivity;
    [SerializeField] float topClamp = 80f;
    [SerializeField] float bottomClamp = -60f;
    [SerializeField] float groundCheckOffset;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpHeight;

    InputReader inputReader;
    CharacterController characterController;
    Transform playerCamera;

    const float gravity = -9.81f;
    float verticalRotation;
    float verticalVelocity;
    bool isGrounded;

    void Start()
    {
        inputReader = GetComponent<InputReader>();
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        GroundCheck();
        Move();
        CameraMovement();
        JumpAndGravity();
    }

    void Move()
    {
        float currentSpeed = inputReader.Sprint ? sprintSpeed : moveSpeed;

        float xMovement = inputReader.Move.x;
        float yMovement = inputReader.Move.y;

        Vector3 movementDirection = (transform.right * xMovement + transform.forward * yMovement).normalized;
        characterController.Move(movementDirection * currentSpeed * Time.deltaTime + new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
    }

    void CameraMovement()
    {
        float mouseX = inputReader.Look.x * sensitivity;
        float mouseY = inputReader.Look.y * sensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation += mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, bottomClamp, topClamp);

        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = verticalRotation;
        playerCamera.eulerAngles = targetRotation;
    }

    void GroundCheck()
    {
        Vector3 groundCheckPosition = new Vector3(transform.position.x, transform.position.y - groundCheckOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(groundCheckPosition, groundCheckRadius, groundLayer);
    }

    void JumpAndGravity()
    {
        if (isGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }

            if (inputReader.Jump)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        verticalVelocity += gravity * 2.5f * Time.deltaTime;
    }
}
