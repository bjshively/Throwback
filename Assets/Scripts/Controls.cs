using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public LayerMask ground;

    protected Rigidbody2D body;

    private float horizontalSpeed = 12.0F;
    private bool grounded;

    private Vector3 startLocation;

    // Use this for initialization
    void Start()
    {
        startLocation = transform.position;

        body = GetComponent<Rigidbody2D>();
        grounded = IsGrounded();      
    }
	
    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = IsGrounded();
        // Stupid simple movement code
        float h = horizontalSpeed * Input.GetAxis("Horizontal");
        if (Mathf.Abs(h) < .2)
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(h, body.velocity.y);    
        }


        if (Input.GetKeyDown("space") && grounded)
        {
            body.AddForce(new Vector2(0, 500), ForceMode2D.Impulse);
        }
            
        if (transform.position.y < -10)
        {
            transform.position = startLocation;
        }

    }

    public bool IsGrounded()
    {
        if (Physics2D.Raycast(transform.FindChild("groundPoint").position, Vector2.down, 0.4f, ground.value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}