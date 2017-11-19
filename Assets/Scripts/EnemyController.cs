using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D body;
    private GameObject player;
    private SpriteRenderer renderer;
    private float facing;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        facing = 1;
    }
	
    // Update is called once per frame
    void Update()
    {

        // Enemies only move when on screen with the player
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
    // If they don't match, flip the enemy sprite to face the player
    private void Move()
    {
        float playerDirection = Mathf.Sign(player.transform.position.x - gameObject.transform.position.x);
        body.velocity = new Vector2(1 * playerDirection, body.velocity.y);
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
