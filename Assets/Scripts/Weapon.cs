﻿using System.Collections;
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

        // Fire the bullet out of the player's position
        // TODO: May want to update to some gun barrel position eventually
        transform.position = player.transform.position;

        // Fire bullets the direction the player is facing
        body.velocity = new Vector2(projectileSpeed * pc.facing, 0);
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

    // Destroy the enemy and bullet on contact
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            gameObject.SetActive(false);
        }
    }

    protected void SelfDestruct()
    {
        Destroy(gameObject);
    }

    protected void ResetFire()
    {
        pc.canFire = true;
    }
}
