﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private LayerMask ground;
    private Vector3 startLocation;
    public Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    public Animator anim;
    private BoxCollider2D melee;

    // Player attributes
    private const int STARTHEALTH = 3;
    public int currentHealth = 3;
    private float moveSpeed = 3F;
    private float jumpForce = 7;

    private GameObject groundPoint1;
    private GameObject groundPoint2;
    private GameObject groundPoint3;

    // State
    public bool grounded;
    private bool invincible;
    public bool canFire;
    private bool scopeIsCool = true;
    private float scopeCooldownTime = 10;
    private bool canMove;
    public float facing;
    public bool alive;

    public bool hasPowerglove = false;
    public bool hasZapper = false;
    public bool hasSuperscope = false;

    // Use this for initialization
    void Start()
    {
        // Combine ground and jumpthrough layers into a binary layermask
        // For detecting if player IsGrounded();
        int groundLayer = 8;
        int jumpthroughLayer = 13;
        ground = (1 << groundLayer) | (1 << jumpthroughLayer);
            
        startLocation = transform.position;
        groundPoint1 = GameObject.Find("groundPoint1");
        groundPoint2 = GameObject.Find("groundPoint2");
        groundPoint3 = GameObject.Find("groundPoint3");

        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        melee = GameObject.Find("melee").GetComponent<BoxCollider2D>();
        melee.enabled = false;
        grounded = IsGrounded();      
        invincible = false;
        canFire = true;
        facing = 1;
        canMove = true;
        alive = true;
    }
	
    // Update is called once per frame
    void Update()
    {
        grounded = IsGrounded();

        // Enable jumpthrough platforms by disabled collissions when player is jumping
        // Layer 10 = player
        // Layer 13 = jumpthrough
        if (body.velocity.y >= 0.05f)
        {
            Physics2D.IgnoreLayerCollision(13, 10, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(13, 10, false);
        }

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
                body.velocity = new Vector2(body.velocity.x, jumpForce);
            }
                
            // Weapons
            if (hasZapper)
            {
                if (Input.GetKey("x") && canFire)
                {
                    // Spawn an instance of the bullet prefab
                    Instantiate(Resources.Load("pistol"));
                    canFire = false;
                    anim.SetTrigger("zap");
                }
            }

            if (hasPowerglove)
            {
                if (Input.GetKey("z") && canMove)
                {
                    Stop();
                    body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    canMove = false;

                    // Start the animation trigger
                    anim.SetTrigger("melee");

                    // After some delay, enable the melee collision box, then disable it
                    Invoke("setMelee", .2f);
                    Invoke("resetMelee", .5f);
                }
            }

            if (hasSuperscope)
            {
                if (Input.GetKey("f") && canFire && scopeIsCool)
                {
                    scopeIsCool = false;
                    Invoke("resetScopeCool", scopeCooldownTime);
                    Instantiate(Resources.Load("superScopeShot"));
                    canFire = false;
                    anim.SetTrigger("scope");
                }
            }
        }

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
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
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
            if (grounded)
            {
                body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            body.velocity = new Vector2(0, body.velocity.y);
            anim.SetBool("run", false);
        }
    }

    // Returns true if character is on the ground
    public bool IsGrounded()
    {
        if (Physics2D.Raycast(groundPoint1.transform.position, Vector2.down, 0.2f, ground.value)
            || Physics2D.Raycast(groundPoint2.transform.position, Vector2.down, 0.2f, ground.value)
            || Physics2D.Raycast(groundPoint3.transform.position, Vector2.down, 0.2f, ground.value))
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

    // Take damage, die if health goes to 0
    public void takeDamage()
    {
        if (!invincible)
        {
            canMove = false;
            Stop();
            Invoke("resetMove", 1);
            currentHealth -= 1;

            if (currentHealth <= 0)
            {
                die();
            }
            else
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
                //knockback();

                // 3 seconds of invincibility
                invincible = true;
                Invoke("resetInvincibility", 3);

            }
        }
    }

    // Reduce lives, respawn if you have lives
    private void die()
    {
        alive = false;
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

    // Reset the cooldown for the scope shot
    public void resetScopeCool()
    {
        scopeIsCool = true;
    }


    private void setMelee()
    {
        melee.enabled = true;
    }

    private void resetMelee()
    {
        resetMove();
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        melee.enabled = false;
    }

    public void collectItem()
    {
        body.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        canMove = false;
        anim.SetBool("item", true);
        Invoke("resetItem", 1);
    }

    private void resetItem()
    {
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        anim.SetBool("item", false);
        canMove = true;
    }

    // Player should repeatedly take damage from hazards if standing on/in them
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.tag == "Hazard")
        {
            takeDamage();
        }
    }
}