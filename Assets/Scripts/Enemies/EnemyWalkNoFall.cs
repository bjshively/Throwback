using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkNoFall : Enemy
{
    
    public LayerMask ground;
    private float timer;

    // Use this for initialization
    void Start()
    {
        base.Start();
        moveSpeed = .5f;
    }

    void Update()
    {
        if (Mathf.Abs(Vector2.Distance(player.transform.position, transform.position)) < 4)
        {
            Move();
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }

    protected override void Move()
    {
        body.velocity = new Vector2(moveSpeed, body.velocity.y);
    }

    protected void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        moveSpeed *= -1;
    }

    // NOTE: After trying a bunch of clever things with raycasting to detect the edge of a platform
    // Putting an invisible trigger turned out to be much easier and more reliable
    // We should use triggers tagged with wall (or something more accurate) to support this sentry
    // functionality.

    void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Turnaround")
        {
            Flip();
        }
    }
}