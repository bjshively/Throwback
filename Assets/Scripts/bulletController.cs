using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{

    private Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        transform.position = GameObject.Find("Player").transform.position;
        body = GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(30, 0);    
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }
}
