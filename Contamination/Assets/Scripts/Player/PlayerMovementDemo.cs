using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class PlayerMovementDemo : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigi;
    //
    //
    void Update()
    {
        
        move(); //from line 26
        jump(); //from line 39
        if (!isWallJumping)
        {
            flip(); //from line 73
        }
        crouch(); //from line 117
        wallSlide();
        wallJump();
<<<<<<< HEAD
<<<<<<< Updated upstream
        flash();
=======
>>>>>>> Stashed changes
=======
>>>>>>> parent of fa44a17 (Bhai ye pata nhi kya ho raha hai)

    }
    //
    //
    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            moving();
        }
       
    }
    //
    //
    //
    //
    //Code for Movement
    public bool airControl;
    public float movementSmoothing=0.05f;
    private Vector2 refVelocity = Vector2.zero;
    private float horizontalMove;
    public float runSpeed = 10f;
    private bool isMoving;
    private void move()
    {
        if (airControl){
            horizontalMove = Input.GetAxisRaw("Horizontal");
            if (horizontalMove > 0 || horizontalMove < 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }
    }
    private void moving()
    {
        Vector2 targetVelocity = new Vector2(horizontalMove * runSpeed, rigi.velocity.y);
        rigi.velocity = Vector2.SmoothDamp(rigi.velocity, targetVelocity, ref refVelocity, movementSmoothing);
    }
    //
    //
    //
    //
    //Code for fliping the Player left and right
    private bool isFacingRight = false;
    private void fliping()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void flip()
    {
        if (isFacingRight && horizontalMove < 0)
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
    //
    //
    //
    //
    //Code for checking if the player is grounded
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    //
    //
    //Code for Jumping
    public float jumpPower = 8f;
    private bool jumping = true;
    private float extraJump = 1;
    private void jump()
    {
        if (isGrounded())
        {
            airControl = true;
            jumping = true;
            extraJump = 1;
        }
        if (jumping)
        {
            if (Input.GetButtonDown("Jump") && isGrounded() && extraJump == 1)
            {
                rigi.velocity = new Vector2(rigi.velocity.x, jumpPower);
            }
            
            if (Input.GetButtonDown("Jump") && !isGrounded() && extraJump > 0)
            {
                rigi.velocity = new Vector2(rigi.velocity.x, jumpPower);
                extraJump = 0;
            }
        }
    }
    //
    //
    //
    //
    //Code for Checking if the player is under a ceiling
    [SerializeField] private Transform ceilingCheck;
    private bool underCeiling()
    {
        return (Physics2D.OverlapCircle(ceilingCheck.position, 0.2f, groundLayer));
    }
    //
    //
    //Code for crouching
    [SerializeField] private Collider2D crouchDisableCollider;
    private float crouchSpeed = 0.5f;
    private bool crouching = false;
    private void isCrouching()
    {
        // If the character has a ceiling preventing them from standing up, keep them crouching
        if (!crouching)
        {
            if (underCeiling())
            {
                crouching = true;
            }
            if (!underCeiling())
            {
                crouching = false;
            }
        }
        //Conditions if the player is crouching
        if (crouching)
        {
            runSpeed *= crouchSpeed;
            jumping = false;
            if (crouchDisableCollider != null)
                crouchDisableCollider.enabled = false;
        }
        //Conitions if the player is not crouching
        else
        {
            runSpeed = 10f;
            jumping = true;
            if (crouchDisableCollider != null)
                crouchDisableCollider.enabled = true;
        }
    }
    //Code for making player crouch when the crouch button is pressed
    private void crouch()
    {
        if (Input.GetButtonDown("Crouch") && isGrounded())
        {
            crouching = true;
            isCrouching();
        }
        if (Input.GetButtonUp("Crouch"))
        {
            crouching = false;
            isCrouching();
        }
    }
    //
    //
    //
    //
    //Code for checking if the player is walled
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    //Checking if the player touches the wall
    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    //
    //
    //Code for WallSliding
    private float wallSlidingSpeed = 2f;
    private bool isWallSliding;
    private void wallSlide()
    {
        if (isWalled())
        {
            rigi.velocity = new Vector2(rigi.velocity.x, Mathf.Clamp(rigi.velocity.y, -wallSlidingSpeed, float.MaxValue));
            if (rigi.velocity.y < 0)
            {
                isWallSliding = true;
            }
        }
        else
        {
            isWallSliding = false;
        }
        

        
    }
    //
    //
    //
    //
    //Code for Walljumping
    private bool isWallJumping;
    private Vector2 wallJumpingPower = new Vector2(-50f, 20f);
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private void wallJump()
    {
        if (isWallSliding)
        {
            Debug.Log("WallSliding");
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(stopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter>0f)   
        {
            isWallJumping = true;
            rigi.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x = -localScale.x;
                transform.localScale = localScale;
            }
            Invoke(nameof(stopWallJumping),0.4f);
        }
        
    }

    private void stopWallJumping()
    {
        Debug.Log("Stopped Wall Jump");
        isWallJumping = false;
    }
<<<<<<< HEAD
<<<<<<< Updated upstream

    [SerializeField] private Light2D flashlight;
    private void flash()
    {
        if (Input.GetButtonDown("Flash"))
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }
=======
>>>>>>> Stashed changes
=======
>>>>>>> parent of fa44a17 (Bhai ye pata nhi kya ho raha hai)
}
