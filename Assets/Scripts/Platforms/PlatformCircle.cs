using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCircle : PlatformMove
{

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
        speed = (Mathf.PI * 2) / timeToCompleteCircle;
    }

    protected override void Move()
    {
        speed = (Mathf.PI * 2) / timeToCompleteCircle;
        currentAngle += Time.deltaTime * speed;
        float newX = radius * Mathf.Cos(currentAngle);
        float newY = radius * Mathf.Sin(currentAngle);
        transform.position = new Vector3(newX, newY, transform.position.z);   
    }
}
