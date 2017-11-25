using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    // TODO: Rewire this to use values from LevelManager and display them

    private GameObject player;
    private PlayerController pc;
    private Text healthText;
    private Text livesText;
    private Animator healthDisplay;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
        healthDisplay = GameObject.Find("healthDisplay").GetComponent<Animator>();


        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthText.text = "Health: " + pc.currentHealth;
        livesText = GameObject.Find("LivesText").GetComponent<Text>();
        //healthText.text = "  `: " + LevelManager.Instance.lives;
    }

    void FixedUpdate()
    {
        healthText.text = "Health: " + pc.currentHealth;
        healthDisplay.SetInteger("health", pc.currentHealth);
        livesText.text = "Lives: " + LevelManager.Instance.lives;
    }
}
