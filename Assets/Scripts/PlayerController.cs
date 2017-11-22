using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask ground;
    private Vector3 startLocation;
    protected Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    // Player attributes
    private const int STARTHEALTH = 3;
    public int currentHealth = 3;
    private int lives = 100;

    public int livesCount
    {
        get { return lives; }
    }

    private float moveSpeed = 3.5F;


    // State
    public bool grounded;
    private bool invincible;
    public bool canFire;
    private bool canMove;
    public float facing;


    // Use this for initialization
    void Start()
    {
        startLocation = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        grounded = IsGrounded();      
        invincible = false;
        canFire = true;
        facing = 1;
        canMove = true;
    }
	
    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = IsGrounded();

        if (canMove)
        {
            if (Input.GetKey("d"))
            {
                Move(1);
            }
            else if (Input.GetKey("a"))
            {
                Move(-1);
            }
            else
            {
                Move(0);
            }
            // Jump       
            if (Input.GetKeyDown("space") && grounded)
            {
                body.AddForce(new Vector2(0, 300), ForceMode2D.Impulse);
            }

            // Fire pistol
            if (Input.GetKey("x") && canFire)
            {
                // Spawn an instance of the bullet prefab
                Instantiate(Resources.Load("pistol"));
                canFire = false;
                anim.SetTrigger("zap");
            }

            // Fire machinegun
            if (Input.GetKey("f") && canFire)
            {
                // Spawn an instance of the bullet prefab
                Instantiate(Resources.Load("superScopeShot"));
                canFire = false;
                anim.SetTrigger("scope");
            }
        }

        // For testing/during dev
        // Respawn player at start location if they fall below y=-10
        if (transform.position.y < -10)
        {
            die();
        }

    }

 

    // Move the player
    private void Move(float h)
    {
        if (Mathf.Abs(h) > 0)
        {
            anim.SetBool("run", true);
            Vector3 scale = transform.localScale;
            body.velocity = new Vector2(moveSpeed * Mathf.Sign(h), body.velocity.y);
            if (body.velocity.x > 0)
            {
                scale.x = Mathf.Abs(scale.x);
                transform.localScale = scale;
                facing = 1;
            }
            else if (body.velocity.x < 0)
            {
                scale.x = Mathf.Abs(scale.x) * -1;
                transform.localScale = scale;
                facing = -1;
            }
        }
        else
        {
            body.velocity = new Vector2(0, body.velocity.y);
            anim.SetBool(("run"), false);

        }
    }

    // Returns true if character is on the ground
    public bool IsGrounded()
    {
        if (Physics2D.Raycast(transform.Find("groundPoint1").position, Vector2.down, 0.1f, ground.value)
            || Physics2D.Raycast(transform.Find("groundPoint2").position, Vector2.down, 0.1f, ground.value)
            || Physics2D.Raycast(transform.Find("groundPoint3").position, Vector2.down, 0.1f, ground.value))
        {
            anim.SetBool("grounded", true);
            return true;
        }
        else
        {
            anim.SetBool("grounded", false);
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
    public void takeDamage()
    {
        if (!invincible)
        {
            canMove = false;
            Stop();
            Invoke("resetMove", 1);

            invincible = true;
            knockback();

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
        resetMove();
        Stop();
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
        // TODO: This needs improved quite a bit, but works better than it did before.
        Vector2 destination = transform.position;
        destination.x = destination.x + (facing * -1);
        destination.y = destination.y + 1;
        transform.position = Vector2.MoveTowards(destination, new Vector2(transform.position.x, transform.position.y), .3f * Time.deltaTime);
        anim.SetTrigger("knockback");

    
    }

    public void Stop()
    {
        body.velocity = Vector2.zero;
    }

    public void resetFire()
    {
        canFire = true;  
    }

    public void resetMove()
    {
        canMove = true;  
    }

    public void startResetFireTimer(float delay)
    {
        Invoke("resetFire", delay);
    }
}