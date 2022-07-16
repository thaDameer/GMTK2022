using UnityEngine;

public class CubePhysics : MonoBehaviour
{
    [SerializeField][Range(0f, -100f)] private float gravity = -40f;
    [SerializeField] private float jumpHeight = 10f;

    private bool isGrounded, isJumping, jumpPressed;
    private float currentVerticalSpeed, currentForwardSpeed;
    private const float GroundCheckDistance = 0.75f;
    private float JumpSpeed => Mathf.Sqrt(-2f * gravity * jumpHeight);
    
    public bool PausePhysics { get; set; }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            TryJump();
        
        CheckGrounded();
        CalculateGravity();
        CheckJump();
        CalculateMovement();
    }

    public void TryJump()
    {
        if (!isJumping)
            jumpPressed = true;
    }

    private void CheckJump()
    {
        if (!jumpPressed || isJumping) return;

        jumpPressed = false;
        isJumping = true;
        currentVerticalSpeed += JumpSpeed;
    }

    private void CalculateGravity()
    {
        if (!isGrounded)
            currentVerticalSpeed += gravity * Time.deltaTime;
    }

    private void CalculateMovement()
    {
        if (PausePhysics) 
            return;
        
        var movement = new Vector3(0, currentVerticalSpeed, currentForwardSpeed) * Time.deltaTime;
        transform.position += movement;
    }
    
    private void CheckGrounded()
    {
        var position = transform.position;
        var notJumping = currentVerticalSpeed <= 0f;
        var ray = new Ray(position, Vector3.down);
        var groundCheck = Physics.Raycast(ray, out var hit, GroundCheckDistance) && notJumping;
        var landedThisFrame = groundCheck && !isGrounded;

        if (!groundCheck)
        {
            isGrounded = false;
            return;
        }

        if (landedThisFrame) 
            AdjustHeightPosition(hit.distance);

        isGrounded = true;
        isJumping = false;
        
        if (currentVerticalSpeed < 0) 
            currentVerticalSpeed = 0f;
    }

    private void AdjustHeightPosition(float hitDistance)
    {
        var extents = transform.lossyScale.y * 0.5f;
        transform.position += Vector3.down * (hitDistance - extents);
    }
}