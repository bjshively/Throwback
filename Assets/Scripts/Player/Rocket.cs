﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody2D body;
    private Animator anim;
    private AudioSource audio;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audio = GetComponent <AudioSource>();
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
        player.UpdateState();
        audio.Play();
        player.gameObject.SetActive(false);
        body.gravityScale = 0;
        anim.SetTrigger("FinishLevel");
        body.velocity = Vector2.up * 5;
        LevelManager.Instance.Invoke("NextLevel", 2.5f);
    }
}