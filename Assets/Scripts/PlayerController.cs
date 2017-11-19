using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask ground;
    private Vector3 startLocation;
    protected Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    // Player attributes
    private const int STARTHEALTH = 3;
    public int currentHealth = 3;
    private int lives = 100;

    public int livesCount
    {
        get { return lives; }
    }

    private float moveSpeed = 5.0F;


    // State
    private bool grounded;
    private bool invincible;
    public bool canFire;
    public float facing;


    // Use this for initialization
    void Start()
    {
        startLocation = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        grounded = IsGrounded();      
        invincible = false;
        canFire = true;
        facing = 1;
    }
	
    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = IsGrounded();

        float h = moveSpeed * Input.GetAxis("Horizontal");
        move(h);
       
        //facing = Mathf.Sign(body.velocity.x);
       
        // Jump       
        if (Input.GetKeyDown("space") && grounded)
        {
            body.AddForce(new Vector2(0, 500), ForceMode2D.Impulse);
        }

        // Fire pistol
        if (Input.GetKey("x") && canFire)
        {
            // Spawn an instance of the bullet prefab
            Instantiate(Resources.Load("pistol"));
            canFire = false;
        }

        // Fire machinegun
        if (Input.GetKey("f") && canFire)
        {
            // Spawn an instance of the bullet prefab
            Instantiate(Resources.Load("machinegun"));
            canFire = false;
        }

        // For testing/during dev
        // Respawn player at start location if they fall below y=-10
        if (transform.position.y < -10)
        {
            die();
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
        if (Physics2D.Raycast(transform.Find("groundPoint").position, Vector2.down, 0.2f, ground.value))
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


    // TODO: Make player and enemy not interact in physics but still damage on collision

    // Take damage, die if health goes to 0
    private void takeDamage()
    {
        if (!invincible)
        {
            knockback();
            invincible = true;
            currentHealth -= 1;
            spriteRenderer.color = new Color(1f, 1f, 1f, .5f);

            if (currentHealth <= 0)
            {
                die();
            }
            else
            {
                // 3 seconds of invincibility
                Invoke("resetInvincibility", 3);

            }
        }
    }

    // Reduce lives, respawn if you have lives
    private void die()
    {
        lives -= 1;
        if (lives > 0)
        {
            respawn();
        }
    }

    // Respawn the character, and reset any necessary state such as health, location
    private void respawn()
    {
        currentHealth = STARTHEALTH;
        resetInvincibility();
        gameObject.transform.position = startLocation;
    }

    // Called after iframes expire
    private void resetInvincibility()
    {
        invincible = false;
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    // Player is knocked backwards upon collidding with an enemy
    // TODO: Ease camera follow so screen doesn't jerk
    private void knockback()
    {
        // TODO: I don't think this works at all right now.
        Vector2 pos = gameObject.transform.position;
        Vector2 newPos = new Vector2(pos.x - 2 * facing, pos.y);
        gameObject.transform.position = newPos;
    }

    public void resetFire()
    {
        canFire = true;  
    }

    public void startResetFireTimer(float delay)
    {
        Invoke("resetFire", delay);
    }
}