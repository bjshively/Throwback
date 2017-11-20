using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    protected Rigidbody2D body;
    protected GameObject player;
    protected SpriteRenderer renderer;
    protected float facing;
    protected float playerDirection;

    public virtual float moveSpeed
    {
        get
        {
            return 3;
        }
    }

    // Use this for initialization
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        facing = 1;
    }

    protected void Update()
    {
        Flip();
        // Enemies only move when visible
        if (renderer.isVisible)
        {
            Move();
        }
    }

    // Might be useful for other on-screen behaviors such as firing, etc.
    void OnBecameVisible()
    {

    }

    // Stop the enemy from flying/walking away when the player is off screen
    void OnBecameInvisible()
    {
        body.velocity = Vector2.zero;
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

}
