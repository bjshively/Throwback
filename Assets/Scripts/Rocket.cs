using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D body;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            Launch();
        }
    }

    void Launch()
    {
        player.SetActive(false);
        body.gravityScale = 0;
        anim.SetTrigger("FinishLevel");
        body.velocity = Vector2.up;
        LevelManager.Instance.Invoke("NextLevel", 2.5f);
    }
}