using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    private Rigidbody2D body;
    private GameObject player;
    protected PlayerController pc;
    protected SpriteRenderer renderer;

    public virtual float projectileSpeed
    {
        get
        {
            return 15;
        }
    }

    protected virtual float fireDelay
    {
        get
        {
            return 1;
        }
    }

    // Use this for initialization
    protected void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();

        // Fire bullet out of barrel location
        Vector2 spawnPosition = GameObject.Find("zapperBarrelPoint").transform.position;
        transform.position = spawnPosition;

        // Fire bullets the direction the player is facing
        body.velocity = new Vector2(projectileSpeed * pc.facing, 0);
        Flip();

        pc.canFire = false;
        pc.startResetFireTimer(fireDelay);
    }

    void FixedUpdate()
    {
        if (!renderer.isVisible)
        {
            SelfDestruct();
        }
    }

    // Flip bullet sprites to match the direction they are being fired
    protected void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= pc.facing;
        transform.localScale = scale;
    }

    // Destroy the enemy and bullet on contact
    protected void OnCollisionEnter2D(Collision2D col)
    {
        // Destroy enemy and bullet on contact
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }

        // Destroy bullet when it hits a wall or other piece of the world
        if (col.gameObject.tag == "World")
        {
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Breakable")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }

    protected void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
