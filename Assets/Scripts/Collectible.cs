using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private PlayerController player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }


    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (gameObject.name == "TetrisRed")
            {
                GameObject.Find("cage").GetComponent<Cage>().Unlock();
                collect();
            }

            // Weapons
            else if (gameObject.name == "PowerGlove")
            {
                player.hasPowerglove = true;
                player.anim.runtimeAnimatorController = Resources.Load("PlayerNoZapper") as RuntimeAnimatorController;
                collect();
            }
            else if (gameObject.name == "Zapper")
            {
                player.anim.runtimeAnimatorController = Resources.Load("Player") as RuntimeAnimatorController;
                player.hasZapper = true;
                collect();
            }
            else if (gameObject.name == "SuperScope")
            {
                player.hasSuperscope = true;
                collect();
            }


        }
    }

    private void collect()
    {
        player.collectItem();
        Destroy(gameObject);
    }
}

