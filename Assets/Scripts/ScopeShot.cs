using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeShot : Weapon
{
    private float moveSpeed = 5;
    private Vector2 target;
    private Rigidbody2D body;
    private SpriteRenderer renderer;
    private GameObject shot;

    private GameObject player;


    public float xOffset;
    public float yOffset;

    new void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
        shot = GameObject.Find("superScopeShot(Clone)");
        // TODO: There are still some circumstances where this doesn't get destroyed/cleaned up properly.
        // Need to put some better safeguards in here to prevent the game getting bogged down.

        moveSpeed *= pc.facing;

        Vector2 spawnPosition = GameObject.Find("zapperBarrelPoint").transform.position;
        transform.position = spawnPosition;
        target = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);


        // Flip bullet sprites to match the direction they are being fired
        Vector2 scale = transform.localScale;
        scale.x *= pc.facing;
        transform.localScale = scale;
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