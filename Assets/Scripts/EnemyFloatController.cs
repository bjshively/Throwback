using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloatController : EnemyController
{
    private float yDirection = 1;
    //    private Time t;
    float timer;

    public virtual float moveSpeed
    {
        get { return 1f; }
    }

    protected void Start()
    {
        base.Start();
        timer = 0;
    }
	
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            yDirection *= -1;
            timer = 0;
        }

        body.velocity = new Vector2(0, moveSpeed * yDirection);
    }
}
