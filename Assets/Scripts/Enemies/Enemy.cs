using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Rigidbody2D body;
    protected PlayerController player;
    protected SpriteRenderer renderer;
    protected float facing;
    protected float playerDirection;
    protected Animator anim;

    protected bool canMove = true;
    public float moveSpeed;

    // Use this for initialization
    protected virtual void Start()
    {
        gameObject.tag = "Enemy";
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        facing = 1;
        moveSpeed = 1;
    }

    protected void Update()
    {
        Flip();

        // Most enemies only move within a certain range of the player
        if (Mathf.Abs(Vector2.Distance(player.transform.position, transform.position)) < 4)
        {
            Move();
        }
        else
        {
            body.velocity = Vector2.zero;
        }
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

    protected void OnCollisionEnter2D(Collision2D col)
    {
        // bullets
        if (col.collider.gameObject.layer == 11)
        {
            Die();
        }
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.takeDamage();
        }

        if (col.gameObject.name == "melee")
        {
            Die();
        }
    }

    protected void SelfDestruct()
    {
        Destroy(gameObject);
    }

    protected void Die()
    {
        Stop();
        body.simulated = false;
        anim.SetTrigger("die");
        Invoke("SelfDestruct", .5f);
    }

    protected void Stop()
    {
        canMove = false;
        moveSpeed = 0;
        body.velocity = Vector2.zero;
    }
}