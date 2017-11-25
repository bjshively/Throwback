using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : Enemy
{
    private float moveSpeed = -1;
    private bool canTurn;
    // Use this for initialization
    void Start()
    {
        base.Start();
        canTurn = true;
    }

    protected override void Move()
    {
        body.velocity = new Vector2(moveSpeed, body.velocity.y);
    }


    // If the enemy bumps into part of the World, change directions
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Breakable")
        {
            if (canTurn)
            {
                moveSpeed *= -1;
                canTurn = false;
                Invoke("resetCanTurn", .5f);

            }
        }
    }

    private void resetCanTurn()
    {
        canTurn = true;
    }
}