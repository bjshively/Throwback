using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : Enemy
{
    
    private bool canTurn;
    // Use this for initialization
    void Start()
    {
        base.Start();
        canTurn = true;
        moveSpeed = -.5f;
    }

    protected override void Move()
    {
        if (canMove)
        {
            body.velocity = new Vector2(moveSpeed, body.velocity.y);
        }
    }


    // If the enemy bumps into part of the World, change directions
    void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);

        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Breakable")
        {
            if (canTurn)
            {
                moveSpeed *= -1;
                canTurn = false;
                Invoke("resetCanTurn", .4f);

            }
        }
    }

    private void resetCanTurn()
    {
        canTurn = true;
    }
}