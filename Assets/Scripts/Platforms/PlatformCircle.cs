using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCircle : PlatformMove
{
    private Vector3 center;
    public float radius = .1f;
    public float timeToCompleteCircle = 10f;
    private float currentAngle = 0f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Awake()
    {
        center = transform.Find("pivotpoint").transform.position;
        speed = (Mathf.PI * 2) / timeToCompleteCircle;
    }

    protected override void Move()
    {
        speed = (Mathf.PI * 2) / timeToCompleteCircle;
        currentAngle += Time.deltaTime * speed;
        Debug.Log(currentAngle);
        float newX = radius * Mathf.Cos(currentAngle) + center.x;
        float newY = radius * Mathf.Sin(currentAngle) + center.y;
        transform.position = new Vector3(newX, newY, transform.position.z);   
    }
}
