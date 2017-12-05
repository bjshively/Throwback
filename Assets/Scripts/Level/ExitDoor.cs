using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private Animator anim;
    private AudioSource audio;
    private PlayerController player;
    private bool gameComplete;
    // Use this for initialization
    void Start()
    {
        gameComplete = false;
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
	
    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (anim.GetBool("complete") && !gameComplete)
            {
                GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
                anim.SetTrigger("eat");
                audio.Play();
                player.Disappear();
                gameComplete = true;
            }
        }
    }
  
}
