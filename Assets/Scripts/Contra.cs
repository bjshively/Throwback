using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contra : Enemy
{
    private float timer = 0;
    private int shotDelay = 2;
    private Vector2 target;
    private Vector3 barrelPoint;
	
    //    // Update is called once per frame
    //    void Update()
    //    {
    //
    //    }

    protected override void Move()
    {
        playerDirection = Mathf.Sign(player.transform.position.x - gameObject.transform.position.x);
        Flip();
        timer += Time.deltaTime;

        // Only shoot when within range of player
        if (Mathf.Abs(Vector2.Distance(player.transform.position, transform.position)) < 4)
        {
            // Enemy has a shot cooldown
            if (timer > shotDelay)
            {
                Shoot();
                timer = 0;
            }
        }
    }

    void Shoot()
    {
        barrelPoint = transform.Find("barrel").transform.position;
        GameObject bullet = Instantiate(Resources.Load("contraBullet") as GameObject);
        bullet.transform.position = barrelPoint;
    }
}
