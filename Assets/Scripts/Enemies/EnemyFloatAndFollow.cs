using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloatAndFollow : Enemy
{
    public virtual float moveSpeed
    {
        get { return .5f; }
    }

    // Float towards the player
    protected override void Move()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), player.transform.position, moveSpeed * Time.deltaTime);
    }

}
