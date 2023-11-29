using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float movementInputDirection;

    private bool isFacingRight = true;
    private bool isGrounded;
    private bool canJump;

    private Rigidbody2D rb;

    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;
    public float variableJumpHeightMultiplier = 0.5f;

    public Transform groundCheck;

    public LayerMask whatIsGround;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        CheckIfCanJump();

        
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckSurroundings() 
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0) 
        {
            canJump = true;
        }
        else 
        {
            canJump = false;
        }
    }

    private void CheckMovementDirection()
    {
        if(isFacingRight && movementInputDirection < 0) 
        {
            Flip();
        }
        else if(!isFacingRight && movementInputDirection > 0) 
        {
            Flip();
        }
        
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump")) 
        {
            Jump();
        }

        if (Input.GetButtonUp("Jump")) 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        
    }

    private void Jump()
    {
        if(canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
    }

    private void ApplyMovement() 
    {
        rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
    
    }

    private void Flip() 
    {
        isFacingRight = !isFacingRight;
        // K‰‰nnet‰‰n pelaaja
        transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
        // Vanha versio pelaajan k‰‰nt‰misest‰
        //transform.Rotate(0.0f, 180.0f, 0.0f); 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}
