using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : Enemy
{
    public virtual float moveSpeed
    {
        get
        {
            return -1;
        }
        set
        {
            moveSpeed *= -1;
        }
    }
    
    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (renderer.isVisible)
        {
            Move();
        }
    }

    protected override void Move()
    {
        body.velocity = new Vector2(moveSpeed, body.velocity.y);
    }


    // If the enemy bumps into part of the World, change directions
    void OnCollisionEnter2D(Collision2D col)
    {
        // TODO: Setting this to the "World" tag crashed unity
        // Need to find some way to differentiate walking on the floor from 
        // Walking into a wall.
        if (col.gameObject.tag == "Wall")
        {
            moveSpeed *= -1;

            // TODO: May need to add some kind of Flip logic here
        }
    }

}
