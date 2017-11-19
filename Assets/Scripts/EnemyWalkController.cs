using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkController : EnemyController
{

    // Use this for initialization
    void Start()
    {
        base.Start();
    }
	
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) > 1)
        {
            playerDirection = Mathf.Sign(player.transform.position.x - gameObject.transform.position.x);
            body.velocity = new Vector2(moveSpeed * playerDirection, body.velocity.y);
            Flip();
        }
    }
}
