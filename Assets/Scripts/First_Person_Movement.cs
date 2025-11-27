using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleFirstPerson : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;

    public Transform playerCamera;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    
    private bool isWalking = false;
    
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip walkSound;
    
    

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Lock cursor in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    // csharp
    void HandleMovement()
    {
        // WASD input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        bool isMoving = new Vector2(x, z).sqrMagnitude > 0.01f;

        Vector3 horizontal = (transform.right * x + transform.forward * z) * moveSpeed;

        // Jump & gravity
        if (controller.isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f; // small push down to stay grounded
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (audioSource != null && jumpSound != null)
            {
                if (isWalking)
                {
                    audioSource.Stop();
                    isWalking = false;
                }
                audioSource.PlayOneShot(jumpSound);
            }
        }

        velocity.y += gravity * Time.deltaTime;

        // Single Move call combining horizontal and vertical motion
        Vector3 movement = horizontal + velocity;
        controller.Move(movement * Time.deltaTime);
        
        if (audioSource != null && walkSound != null)
        {
            if (controller.isGrounded && isMoving)
            {
                if (!isWalking)
                {
                    audioSource.clip = walkSound;
                    audioSource.loop = true;
                    audioSource.Play();
                    isWalking = true;
                }
            }
            else
            {
                if (isWalking)
                {
                    audioSource.Stop();
                    isWalking = false;
                }
            }
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // rotate camera up/down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // rotate player left/right
        transform.Rotate(Vector3.up * mouseX);
    }
}