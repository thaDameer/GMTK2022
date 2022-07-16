using UnityEngine;

public class CubePhysics : MonoBehaviour
{
    [SerializeField][Range(0f, -50f)] private float gravity = -5f;
    [SerializeField] private float jumpHeight = 10f;

    private bool isGrounded, isJumping, jumpPressed;
    private float currentVerticalSpeed, currentForwardSpeed;
    private float JumpSpeed => Mathf.Sqrt(-2f * gravity * jumpHeight);

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !isJumping)
            jumpPressed = true;
        
        CheckGrounded();
        CalculateGravity();
        CheckJump();
        CalculateMovement();
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
        var movement = new Vector3(0, currentVerticalSpeed, currentForwardSpeed) * Time.deltaTime;
        transform.position += movement;
    }
    
    private void CheckGrounded()
    {
        var ray = new Ray(transform.position, Vector3.down);
        var groundCheck = Physics.Raycast(ray, out var hit, 0.75f) && currentVerticalSpeed <= 0f;
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