using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkFollow : Enemy
{
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
