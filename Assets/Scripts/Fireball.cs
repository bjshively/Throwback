using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Vector3 target;
    private float moveSpeed;
    private PlayerController pc;


    // Use this for initialization
    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        target = GameObject.Find("Player").transform.position;
        moveSpeed = 2;
    }
	
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target, moveSpeed * Time.deltaTime);
        if (transform.position == target)
        {
            Destroy(gameObject);
        }
    }

    // Destroy when bullet hits the ground
    //    void OnCollisionEnter2D(Collision2D col)
    //    {
    //        if (col.gameObject.layer == 8)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }

    // Damage the player on contact
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            pc.takeDamage();
        }

        if (col.gameObject.tag == "Wall" || col.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
