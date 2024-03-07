using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveScript : MonoBehaviour
{
    // Rigidbody for character movement
    public Rigidbody2D myRb;

    // Maximum speed the character can move
    public float maxSpeed;

    // Acceleration for character movement
    public float acceleration;

    // Check if the character is grounded
    public bool isGrounded;

    // Force applied when the character jumps
    public float jumpForce;

    // Force applied for secondary jump
    public float secondaryJumpForce;

    // Delay between primary and secondary jumps
    public float secondaryJumpDelay;

    // Flag indicating if secondary jump is available
    public bool secondaryJump;

    // Animator for character animation
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get Animator component
        anim = gameObject.GetComponentInChildren<Animator>();

        // Get Rigidbody2D component
        myRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update animation parameter based on character's horizontal speed
        anim.SetFloat("speed", Mathf.Abs(myRb.velocity.x));

        if (Input.GetAxis("Horizontal") < 0)
        {
            anim.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            anim.transform.localScale = new Vector3(1, 1, 1);
        }
       
        // Apply horizontal movement force if not at maximum speed and horizontal input is given
        if (Mathf.Abs(myRb.velocity.magnitude) < maxSpeed && (Mathf.Abs(Input.GetAxis("Horizontal")) > 0))
        {
            myRb.AddForce(new Vector2(acceleration * Input.GetAxis("Horizontal"), 0), ForceMode2D.Force);
        }

        // Check for primary jump input and whether the character is grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Apply jump force
            myRb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            // Start coroutine for secondary jump
            StartCoroutine(SecondaryJump());
        }

        // Check for secondary jump input and if secondary jump is available and the character is not grounded
        if (Input.GetButton("Jump") && secondaryJump && !isGrounded)
        {
            // Apply secondary jump force
            myRb.AddForce(new Vector2(0, secondaryJumpForce), ForceMode2D.Force);
        }
    }
    
    // Triggered while character stays on a collider
    private void OnTriggerStay2D(Collider2D other)
    {
        // Set grounded status to true
        isGrounded = true;
    }
    
    // Triggered while character exits a collider
    private void OnTriggerExit2D(Collider2D other)
    {
        // Set grounded status to false
        isGrounded = false;
    }

    // Coroutine for handling secondary jump
    IEnumerator SecondaryJump()
    {
        // Set secondary jump flag to true
        secondaryJump = true;

        // Wait for the specified delay
        yield return new WaitForSeconds(secondaryJumpDelay);

        // Reset secondary jump flag to false
        secondaryJump = false;

        yield return null;
    }
}