using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contra : Enemy
{
    private float timer = 0;
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
        if (timer > 2)
        {
            Shoot();
            timer = 0;
        }
    }

    void Shoot()
    {
        barrelPoint = transform.Find("barrel").transform.position;
        GameObject bullet = Instantiate(Resources.Load("fireball") as GameObject);
        bullet.transform.position = barrelPoint;
    }
}
