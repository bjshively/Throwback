using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformNoParent : MonoBehaviour
{
    protected float speed = 3;
    protected Transform currentPoint;
    public Transform[] points;
    private float timer;
    public float pause;

    // Begin by traveling towards the end point
    public int pointSelection = 1;

    protected void Start()
    {
        timer = 0;
        currentPoint = points[pointSelection];
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.Instance.stopAllAction())
        {
            
            Move();
        }
    }

    protected virtual void Move()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.transform.position, speed * Time.deltaTime);
        if (transform.position == currentPoint.transform.position)
        {
            timer += Time.deltaTime;
            if (timer >= pause)
            {
                Flip();
                timer = 0;
            }
        }
    }

    private void Flip()
    {
        pointSelection++;
        if (pointSelection == points.Length)
        {
            pointSelection = 0;

        }
        currentPoint = points[pointSelection];
    }
}
