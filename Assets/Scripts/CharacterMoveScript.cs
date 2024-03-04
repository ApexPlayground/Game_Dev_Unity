using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveScript : MonoBehaviour
{
    public Rigidbody2D myRb;
    public float maxSpeed;
    public float acceleration;
    public bool isGrounded;
    public float jumpForce;
    public float secondaryJumpForce;
    public float secondaryJumpDelay;
    public bool secondaryJump;
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        myRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", Mathf.Abs(myRb.velocity.x));
        if (Mathf.Abs(myRb.velocity.magnitude) < maxSpeed && (Mathf.Abs(Input.GetAxis("Horizontal")) > 0))
        {
            myRb.AddForce(new Vector2(acceleration * Input.GetAxis("Horizontal"),0), ForceMode2D.Force);
        }

        if (Input.GetButtonUp("Jump") && isGrounded)
        {
            myRb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            StartCoroutine(SecondaryJump());
        }

        if (Input.GetButton("Jump") && secondaryJump == true && isGrounded == false)
        {
            myRb.AddForce(new Vector2(0, secondaryJumpForce), ForceMode2D.Force);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        isGrounded = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
    }

    IEnumerator SecondaryJump()
    {
        secondaryJump = true;
        yield return new WaitForSeconds(secondaryJumpDelay);
        secondaryJump = false;
        yield return null;
    }
}
