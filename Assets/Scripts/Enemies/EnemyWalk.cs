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
        moveSpeed = -3f;
    }

    protected override void Move()
    {
        if (canMove)
        {
            body.velocity = new Vector2(moveSpeed, body.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 14)
        {
            if (canTurn)
            {
                moveSpeed *= -1;
                canTurn = false;
                Invoke("resetCanTurn", .4f);

            }
        }

        // bullets
        if (col.collider.gameObject.layer == 11)
        {
            Die();
        }

    }

    private void resetCanTurn()
    {
        canTurn = true;
    }
}