using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    private float movementInputDirection;


    private bool isFacingRight = true;
    private bool Walk;
    private bool isGrounded;
    private bool canJump;

    private bool canDodge = true;
    private bool isDodging;
    private float dodgingPower = -24f;
    private float dodgingTime = 0.1f;
    private float dodgingCooldown = 1f;

    private Animator animator;
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
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isDodging)
        {
            return;
        }


        CheckInput();
        CheckMovementDirection();
        CheckIfCanJump();
        UpdateAnimations();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDodge)
        {
            StartCoroutine(Dodge());
        }

    }


    private void FixedUpdate()
    {
        if (isDodging)
        {
            return;
        }

        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if (isGrounded /*&& rb.velocity.y <= 0*/)
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
        if (isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if(rb.velocity.x != 0)
        {
            Walk = true;
        }
        else
        {
            Walk = false;
        }

    }

    private void UpdateAnimations()
    {
        animator.SetBool("Walk", Walk);
        animator.SetBool("isGrounded", isGrounded);
    }


    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
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
        if (canJump)
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


    // Alternate dodge method
    private IEnumerator Dodge()
    {
        canDodge = false;
        animator.SetBool("Dodge", true);
        isDodging = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dodgingPower, 0f);
        yield return new WaitForSeconds(dodgingTime);
        rb.gravityScale = originalGravity;
        isDodging = false;
        yield return new WaitForSeconds(dodgingCooldown);
        canDodge = true;
        animator.SetBool("Dodge", false);
    }






    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}
