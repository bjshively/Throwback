using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    private PlayerController player;
    private Notification notifyText;
    private AudioSource sfx;
    private SpriteRenderer renderer;
    private BoxCollider2D col;
    private string attackKey = null;
    private bool collected = false;

    private delegate void PlayerAttack();

    private PlayerAttack attack;

    // How long to display each collectible's notification
    private float messageDisplayTime = 2f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        notifyText = GameObject.Find("Notification").GetComponent<Notification>();
        sfx = GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }
	
    // Update is called once per frame
    void Update()
    {
        // Once the object has been collected, tear it down
        if (collected)
        {
            // Once the item is collected, we set a key and a player method to be executed
            // The player is frozen until they press the key, executing the attack and tearing down the item
            if (attackKey != null)
            {
                if (Input.GetButtonDown(attackKey))
                {
                    attack();
                    Teardown();
                }
            }
            else
            {
                // For items with no action, teardown after a fixed delay
                Invoke("Teardown", messageDisplayTime);
            }
        }
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && collected == false)
        {
            collected = true;
            // Attempt to play audio
            if (sfx)
            {
                sfx.Play();
            }

            if (gameObject.tag == "piece" || gameObject.tag == "key")
            {
                LevelManager.Instance.Invoke("AddPiece", 2);
                notifyText.show("\n\nYou found a strange relic");
                collect(2f);

                if (gameObject.tag == "key")
                {
                    GameObject.Find("Cage").GetComponent<Cage>().Invoke("setOpen", 2.18f);
                }
            }

            if (gameObject.name == "MessageNoWeapon")
            {
                GameObject.Find("ExitDoor").transform.position = new Vector2(328f, 15.65f);
                collected = false;
                notifyText.show("You don't have any weapons.\n\nBe careful.");
                notifyText.Invoke("clearMessage", 4);
                Invoke("SelfDestruct", 3);
            }

            if (gameObject.name == "MessageDoor")
            {
                collected = false;
                notifyText.show("An unusual door...\n\nIt won't budge.");
                notifyText.Invoke("clearMessage", 1.5f);
                Invoke("SelfDestruct", 3);
            }

            // Weapons
            else if (gameObject.name == "PowerGlove")
            {
                attackKey = "Melee";
                notifyText.show("Powerglove\n\n\nPress J to melee");
                player.setLevel(1);
                collect(messageDisplayTime);
                attack = new PlayerAttack(player.UseMelee);
            }
            else if (gameObject.name == "Zapper")
            {
                attackKey = "Zapper";
                notifyText.show("Zapper\n\n\nPress K to fire");
                player.setLevel(2);
                collect(messageDisplayTime);
                attack = new PlayerAttack(player.FireZapper);
            }
            else if (gameObject.name == "SuperScope")
            {
                attackKey = "SuperScope";
                notifyText.show("SuperScope\n\nPress L to fire\n\nIt's powerful but slow to reload");
                player.setLevel(3);
                collect(messageDisplayTime);
                attack = new PlayerAttack(player.FireSuperScope);
            }
        }
    }

    private void collect(float t)
    {
        player.collectItem(t);
        if (renderer != null)
        {
            renderer.enabled = false;
        }
        col.enabled = false;
    }

    private void read()
    {
        
        col.enabled = false;   
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }

    private void Teardown()
    {
        notifyText.clearMessage();
        player.resetItem();
        SelfDestruct();
    }
}