using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalMove;
    public float runSpeed = 10f;

    [SerializeField] private Rigidbody2D rigi;


    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        jump();
        crouch();
        
        wallJump();
        if (!isWallJumping)
        {
            flip();
        }
    }
    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rigi.velocity = new Vector2(horizontalMove * runSpeed, rigi.velocity.y);
        }
        wallSlide();
    }


    //Code for checking if the player is on ground
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool grounded;
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //Code for Jumping.
    public float jumpPower = 20f;
    private bool jumping = true;
    private float extraJump = 1f;
    private void jump()
    {
        if (jumping)
        {
            if (isGrounded())
            {
                jumping = true;
                extraJump = 1;
            }
            if (Input.GetButtonDown("Jump") && isGrounded() && extraJump==1)
            {
                rigi.velocity = new Vector2(rigi.velocity.x, jumpPower);
                
            }
            if (Input.GetButtonUp("Jump") && rigi.velocity.y > 0f)
            {
                rigi.velocity = new Vector2(rigi.velocity.x, rigi.velocity.y * 0.6f);
            }
            if (Input.GetButtonDown("Jump") && !isGrounded() && extraJump > 0)
            {
                rigi.velocity = new Vector2(rigi.velocity.x, jumpPower);
                extraJump = 0;
            }
        }        
    }

    //Code for Crouching.
    [SerializeField] private Collider2D crouchDisableCollider;
    [SerializeField] private Transform ceilingCheck;
    private bool crouchCounter = true;
    private bool crouching;
    private bool ceiling = false;

    private void isCrouching()
    {
        // If the character has a ceiling preventing them from standing up, keep them crouching
        if (Physics2D.OverlapCircle(ceilingCheck.position, 0.2f, groundLayer))
        {
            crouching = true;
            ceiling = true;
        }
        else
        {
            ceiling = false;
        }

        if (crouching)
        {
            runSpeed = 5f;
            jumping = false;
            if (crouchDisableCollider != null)
                crouchDisableCollider.enabled = false;
        }
        else
        {
            runSpeed = 10f;
            jumping = true;
            if (crouchDisableCollider != null)
                crouchDisableCollider.enabled = true;
            
        }
    }
    private void crouch()
    {
        
        if (Input.GetButtonDown("Crouch")&& isGrounded())
        {
            if (crouchCounter)
            {
                crouching = true;
                isCrouching();
                crouchCounter = false;
            }
            else
            {
                crouching = false;
                isCrouching();
                crouchCounter = true;
            }
            
        }

    }

    
    private float wallSlidingSpeed =2f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    //Checking if the player touches the wall
    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        
    }
    //Code for Wallslide
    private bool isWallSliding;
    private void wallSlide()
    {
        if(isWalled())
        {
            isWallSliding = true;
            rigi.velocity = new Vector2(rigi.velocity.x, Mathf.Clamp(rigi.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }
    //Code for Walljumping
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuratiion=0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);
    private void wallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(stopWallJumping));
        }
        else
        {
            wallJumpingCounter =0;
        }
        if(Input.GetButtonDown("Jump")&& wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rigi.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            /*if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
            }*/
            Invoke(nameof(stopWallJumping), wallJumpingDuratiion);
        }
    }

    /*private bool WasWalled;
    private void wasWalled()
    {
        if (isWalled())
        {
            WasWalled = true;
        }
        if (isGrounded())
        {
            WasWalled = false;
        }
    }
    private void canDoubleJump()
    {
        if (WasWalled)
        {
            extraJump = 0;
        }
        else
        {
            extraJump = 2;
        }
    }*/

    private void stopWallJumping()
    {
        isWallJumping = false;
    }

    //Code for flipping the player
    private bool isFacingRight = false;
    private void fliping()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void flip()
    {
        if(isFacingRight && horizontalMove < 0)
        {
            isFacingRight = false;
            fliping();            
        }
        if (!isFacingRight && horizontalMove > 0)
        {
            isFacingRight = true;
            fliping();
        }
    }
    

}
