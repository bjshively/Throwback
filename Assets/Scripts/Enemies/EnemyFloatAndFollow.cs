using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloatAndFollow : Enemy
{

    private Vector2 targetLocation;
    private float timer;
    private AudioSource[] audio;
    private bool canSound;

    public virtual float moveSpeed
    {
        get { return .5f; }
    }

    void Start()
    {
        base.Start();
        audio = GetComponents<AudioSource>();
        canSound = true;
        UpdateTarget();
    }

    void Update()
    {
        if (!LevelManager.Instance.stopAllAction())
        {
            if (Mathf.Abs(Vector2.Distance(player.transform.position, transform.position)) < 6)
            {
                Move();
                // Every 2 seconds, re-pinpoint the target (player)
                timer += Time.deltaTime;
                if (timer > 2)
                {
                    UpdateTarget();
                    timer = 0;
                }
            }
            else
            {
                body.velocity = Vector2.zero;
            }
        }
        else
        {
            //Stop the enemy if stopAllAction is true
            body.velocity = Vector2.zero;
        }
    }

    // Float towards the player
    protected override void Move()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), targetLocation, moveSpeed * Time.deltaTime);
    }

    // Update the reference to the player's position for following
    protected void UpdateTarget()
    {
        targetLocation = player.transform.position;
    }

    void OnBecameVisible()
    {
        
        if (canSound)
        {
            audio[1].Play();
            canSound = false;
            Invoke("resetCanSound", 5);
        }

    }

    void resetCanSound()
    {
        canSound = true;
    }

    protected void Die()
    {
        audio[0].Play();
        alive = false;
        Stop();
        body.simulated = false;
        anim.SetTrigger("die");
        Invoke("SelfDestruct", .5f);
    }
}
