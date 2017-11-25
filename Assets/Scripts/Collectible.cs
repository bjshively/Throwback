using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    private PlayerController player;
    private Notification notifyText;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        notifyText = GameObject.Find("Notification").GetComponent<Notification>();
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
                GameObject.Find("Cage").GetComponent<Cage>().Unlock();
                collect();
            }

            // Weapons
            else if (gameObject.name == "PowerGlove")
            {
                notifyText.show("You got the Powerglove.\n\n\nPress Z to melee obstacles and small enemies.");
                player.hasPowerglove = true;
                player.anim.runtimeAnimatorController = Resources.Load("PlayerNoZapper") as RuntimeAnimatorController;
                collect();
                notifyText.Invoke("clearMessage", 3);
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

