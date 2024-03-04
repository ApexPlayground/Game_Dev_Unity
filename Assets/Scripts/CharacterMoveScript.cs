using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveScript : MonoBehaviour
{
    public Rigidbody2D myRb;
    public float maxSpeed;
    public float acceleration;
    
    // Start is called before the first frame update
    void Start()
    {
        myRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(myRb.velocity.magnitude) < maxSpeed && (Mathf.Abs(Input.GetAxis("Horizontal")) > 0))
        {
            myRb.AddForce(new Vector2(acceleration * Input.GetAxis("Horizontal"),0), ForceMode2D.Force);
        }
    }
}
