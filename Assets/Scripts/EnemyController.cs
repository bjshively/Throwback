using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
	
    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(-1, body.velocity.y);
    }


    void OnBecameVisible()
    {

    }

}
