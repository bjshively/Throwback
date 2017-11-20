﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloat : Enemy
{
    private float yDirection = 1;
    float timer;

    public virtual float moveSpeed
    {
        get { return .5f; }
    }

    protected void Start()
    {
        base.Start();
        timer = 0;
    }

    void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        playerDirection = Mathf.Sign(player.transform.position.x - gameObject.transform.position.x);
        Flip();
        timer += Time.deltaTime;
        if (timer > 2)
        {
            yDirection *= -1;
            timer = 0;
        }

        body.velocity = new Vector2(0, moveSpeed * yDirection);
    }
}