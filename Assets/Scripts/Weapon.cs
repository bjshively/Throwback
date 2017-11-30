using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    private Rigidbody2D body;
    protected PlayerController player;
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
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        // Fire bullet out of barrel location
        Vector2 spawnPosition = GameObject.Find("zapperBarrelPoint").transform.position;
        transform.position = spawnPosition;

        // Fire bullets the direction the player is facing
        body.velocity = new Vector2(projectileSpeed * player.facing, 0);

        // Flip bullet sprites to match the direction they are being fired
        Vector2 scale = transform.localScale;
        scale.x *= player.facing;
        transform.localScale = scale;
        player.canFire = false;
        player.startResetFireTimer(fireDelay);
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(Vector2.Distance(transform.position, player.transform.position)) > 4)
        {
            SelfDestruct();
        }
    }

    // Destroy the enemy and bullet on contact
    void OnCollisionEnter2D(Collision2D col)
    {
        // Destroy enemy and bullet on contact
        if (col.gameObject.tag == "Enemy")
        {
            //  Destroy(col.gameObject);
            SelfDestruct();
        }

        // Destroy bullet when it hits the ground layer
        if (col.gameObject.layer == 8 || col.gameObject.tag == "Wall")
        {
            SelfDestruct();
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            SelfDestruct();
            col.gameObject.GetComponent<Enemy>().Die();
        }
    }

    protected void SelfDestruct()
    {
        Destroy(gameObject);
    }
}