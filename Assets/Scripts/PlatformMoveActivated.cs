using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveActivated : PlatformMove
{
    private bool activated = false;
    private bool reachedTheEnd = false;
    private Animator anim;

    void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Move()
    {
        // If the platform has been activated and isn't at the end yet, move
        if (activated)
        {
            // Once activated, move the platform until it reaches the end
            if (!reachedTheEnd)
            { 
                transform.position = Vector2.MoveTowards(transform.position, currentPoint.transform.position, speed * Time.deltaTime);
                if (transform.position == currentPoint.transform.position)
                {
                    Flip();
                }
                // Once the platform reaches the send, self destruct
            }
            else
            {
                anim.SetTrigger("die");
                Invoke("SelfDestruct", 2);
            }
        }
    }

    // Use this to update the path to the next point, and determine when we've reached the end
    private void Flip()
    {
        pointSelection++;
        if (pointSelection == points.Length)
        {
            reachedTheEnd = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.transform.SetParent(transform);
            activated = true;
        }
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
