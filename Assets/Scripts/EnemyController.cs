using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D body;
    private GameObject player;
    private SpriteRenderer renderer;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        Debug.Log(Screen.width);
    }
	
    // Update is called once per frame
    void Update()
    {

        // Enemies only move when on screen with the player
        if (renderer.isVisible)
        {
            // Figure out which direction is towards the player, and move that direction
            float direction = Mathf.Sign(player.transform.position.x - gameObject.transform.position.x);
            body.velocity = new Vector2(1 * direction, body.velocity.y);
        }
    }

    // Might be useful for other on-screen behaviors such as firing, etc.
    void OnBecameVisible()
    {

    }

}
