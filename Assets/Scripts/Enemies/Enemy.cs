using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Rigidbody2D body;
    protected GameObject player;
    protected PlayerController pc;
    protected SpriteRenderer renderer;
    protected float facing;
    protected float playerDirection;

    public virtual float moveSpeed
    {
        get
        {
            return 1;
        }
    }

    // Use this for initialization
    protected virtual void Start()
    {
        gameObject.tag = "Enemy";
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
        facing = 1;
    }

    protected void Update()
    {
        Flip();
        // Enemies only move when visible
        if (Mathf.Abs(Vector2.Distance(pc.transform.position, transform.position)) < 4)
        {
            Move();
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }

    // Might be useful for other on-screen behaviors such as firing, etc.
    void OnBecameVisible()
    {
        
    }

    // Stop the enemy from flying/walking away when the player is off screen
    void OnBecameInvisible()
    {
        
    }
   
    // Determine direction of player and compare to the direction the enemy is facing
    protected abstract void Move();

    // Flip the enemy sprite to face the player if needed
    protected void Flip()
    {
        if (playerDirection != facing)
        {
            // Flip enemy sprite
            Vector2 scale = transform.localScale;
            scale.x = scale.x *= -1;
            transform.localScale = scale;

            // Update facing value
            facing = Mathf.Sign(scale.x);
        }
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            pc.takeDamage();
        }

        if (col.gameObject.name == "melee")
        {
            Destroy(gameObject);
        }
    }
}