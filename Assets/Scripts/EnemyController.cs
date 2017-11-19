using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Rigidbody2D body;
    protected GameObject player;
    protected SpriteRenderer renderer;
    protected float facing;
    protected float playerDirection;

    public virtual float moveSpeed
    {
        get
        {
            return 3;
        }
    }

    // Use this for initialization
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        facing = 1;
    }
	
    // Update is called once per frame
    void Update()
    {

        // Enemies only move when visible
        if (renderer.isVisible)
        {
            Move();
        }
    }

    // Might be useful for other on-screen behaviors such as firing, etc.
    void OnBecameVisible()
    {

    }

    // Determine direction of player and compare to the direction the enemy is facing
    private void Move()
    {
        if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) > 1)
        {
            playerDirection = Mathf.Sign(player.transform.position.x - gameObject.transform.position.x);
            body.velocity = new Vector2(moveSpeed * playerDirection, body.velocity.y);
            Flip();
        }
    }

    // Flip the enemy sprite to face the player if needed
    private void Flip()
    {
        if (playerDirection != facing)
        {
            // Flip enemy sprite
            Vector2 scale = transform.localScale;
            scale.x = scale.x *= -1;
            transform.localScale = scale;

            // Update facing value
            facing = Mathf.Sign(scale.x);
        }
    }

}
