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

    }
    private void FixedUpdate()
    {
        rigi.velocity = new Vector2(horizontalMove * runSpeed, rigi.velocity.y);
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
        
        if (Input.GetButtonDown("Crouch"))
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

    

}
