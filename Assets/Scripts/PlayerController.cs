using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask ground;
    private Vector3 startLocation;
    protected Rigidbody2D body;



    // Player attributes
    private const int STARTHEALTH = 3;
    private int currentHealth = 3;
    private int lives = 100;
    private float horizontalSpeed = 12.0F;
    private bool grounded;

    public float facing;


    // Use this for initialization
    void Start()
    {
        startLocation = transform.position;
        body = GetComponent<Rigidbody2D>();
        grounded = IsGrounded();      
        facing = 1;
    }
	
    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = IsGrounded();


        float h = horizontalSpeed * Input.GetAxis("Horizontal");
        move(h);
       
        //facing = Mathf.Sign(body.velocity.x);
       
        // Jump       
        if (Input.GetKeyDown("space") && grounded)
        {
            body.AddForce(new Vector2(0, 500), ForceMode2D.Impulse);
        }

        // Shoot
        if (Input.GetKeyDown("x"))
        {
            // Spawn an instance of the bullet prefab
            Instantiate(Resources.Load("bullet"));
        }

        // For testing/during dev
        // Respawn player at start location if they fall below y=-10
        if (transform.position.y < -10)
        {
            transform.position = startLocation;
        }

    }

    // Move the player
    private void move(float h)
    {
        if (Mathf.Abs(h) < .2)
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
        else
        {
            Vector3 scale = transform.localScale;
            body.velocity = new Vector2(h, body.velocity.y);
            if (body.velocity.x > 0)
            {
                scale.x = 1;
                transform.localScale = scale;
                facing = 1;
            }
            else if (body.velocity.x < 0)
            {
                scale.x = -1;
                transform.localScale = scale;
                facing = -1;
            }

        }
    }

    // Returns true if character is on the ground
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            takeDamage();
        }
    }

    // take damage, die if health goes to 0
    private void takeDamage()
    {
        this.currentHealth -= 1;
        if (currentHealth <= 0)
        {
            die();
        }
    }

    // Reduce lives, respawn if you have lives
    private void die()
    {
        lives -= -1;
        if (lives > 0)
        {
            respawn();
        }
    }

    private void respawn()
    {
        currentHealth = STARTHEALTH;
        gameObject.transform.position = startLocation;
    }
}