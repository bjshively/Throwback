﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D body;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }
	
    // Update is called once per frame
    void Update()
    {
        float direction = Mathf.Sign(player.transform.position.x - gameObject.transform.position.x);
        body.velocity = new Vector2(1 * direction, body.velocity.y);
    }


    void OnBecameVisible()
    {

    }

}
