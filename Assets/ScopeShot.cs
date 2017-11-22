using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeShot : Weapon
{
    private float moveSpeed = 3;
    private Vector2 target;
    private Rigidbody2D body;
    private SpriteRenderer renderer;
    private GameObject shot;

    private GameObject player;
    protected PlayerController pc;

    public float xOffset;
    public float yOffset;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
        shot = GameObject.Find("superScopeShot");


        moveSpeed *= pc.facing;

        Vector2 spawnPosition = GameObject.Find("zapperBarrelPoint").transform.position;
        transform.position = spawnPosition;
        target = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);


        // Flip bullet sprites to match the direction they are being fired
        Vector2 scale = transform.localScale;
        scale.x *= pc.facing;
        transform.localScale = scale;

        // TODO: This weapon is pretty powerful so likely it should have a long cooldown
        // But we don't want to completely lockout firing
        // Need to create a separate cooldown for the weapon from canFire.
        pc.canFire = false;
        pc.startResetFireTimer(fireDelay);

        Invoke("SelfDestruct", 5);
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target, moveSpeed * Time.deltaTime);
    }

    protected void SelfDestruct()
    {
        Destroy(gameObject);
        Destroy(shot);
    }
}