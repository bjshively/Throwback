using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    private PlayerController player;
    private Notification notifyText;
    private AudioSource audio;
    private SpriteRenderer renderer;
    private BoxCollider2D col;

    // How long to display each collectible's notification
    private float messageDisplayTime = 4f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        notifyText = GameObject.Find("Notification").GetComponent<Notification>();
        audio = GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Attempt to play audio
            if (audio)
            {
                audio.Play();
            }

            // TODO: Create a "key" tag that can be applied to any collectible to make it unlock the rocket
            if (gameObject.tag == "key")
            {
                GameObject.Find("Cage").GetComponent<Cage>().Invoke("Unlock", 2.18f);
                notifyText.show("Escape unlocked", 2f);
                collect();
            }

            // Weapons
            else if (gameObject.name == "PowerGlove")
            {
                notifyText.show("Powerglove\n\n\nPress Z to melee obstacles and small enemies.", messageDisplayTime);
                player.hasPowerglove = true;
                player.anim.runtimeAnimatorController = Resources.Load("PlayerNoZapper") as RuntimeAnimatorController;
                collect();
            }
            else if (gameObject.name == "Zapper")
            {
                notifyText.show("Zapper\n\n\nPress X to fire.", messageDisplayTime);
                player.anim.runtimeAnimatorController = Resources.Load("Player") as RuntimeAnimatorController;
                player.hasZapper = true;
                collect();
            }
            else if (gameObject.name == "SuperScope")
            {
                notifyText.show("SuperScope\n\nPress F to fire.\n\nIt's powerful but slow to reload.", messageDisplayTime);
                player.hasSuperscope = true;
                collect();
            }


        }
    }

    private void collect()
    {
        player.collectItem();
        renderer.enabled = false;
        col.enabled = false;
        Invoke("SelfDestruct", 3);

    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }


}

