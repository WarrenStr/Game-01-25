using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody rb;
    private InputAction jumpAction;

    private float movementX;
    private float movementY;
    private float camForward;
    [SerializeField] private Transform camTransform;
    
    private float speedRef;

    public bool isGrounded = true;

    public float speed;
    public float rotationSpeed;
    public float jumpForce;
    public float gravityMultiplier;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speedRef = speed;
        rb = GetComponent<Rigidbody>();
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        GroundCheck();
        Jump();
    }

    void FixedUpdate()
    {
       Move();
    }


    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void Move()
    {
        Vector3 cameraForward = camTransform.forward;
        cameraForward.y = 0; // Ignore Y-axis to keep the ball on the ground
        cameraForward.Normalize();

        Vector3 cameraRight = camTransform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        movement = cameraRight * movementX + cameraForward * movementY;

        if (isGrounded)
        {
            speed = speedRef;
        }
        else if (!isGrounded)
        {
            speed = jumpForce;
        }
        
        rb.AddForce(movement * speed);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void Jump()
    {
        if (jumpAction.IsPressed())
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                rb.linearVelocity += Vector3.up * Physics.gravity.y * (gravityMultiplier - 1) * Time.deltaTime;                
            }
        }
    }

    public void GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
 