using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    private Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        // Bullets are destroyed after 1 second automatically
        Destroy(gameObject, 5);

        // Fire the bullet out of the player's position
        // May want to update to some gun barrel position eventually
        transform.position = GameObject.Find("Player").transform.position;

        body = GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(15, 0);    
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    // Destroy the enemy and bullet when a collission occurs
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
