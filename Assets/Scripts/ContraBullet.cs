using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContraBullet : MonoBehaviour
{
    private Vector3 target;
    private float moveSpeed;
    private PlayerController player;


    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        target = player.transform.position;
        moveSpeed = 2;

        // All bullets will self destruct after 2 seconds
        Invoke("SelfDestruct", 2);
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

    // Damage the player on contact
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Wall" || col.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }

    protected void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.takeDamage();
        }
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
