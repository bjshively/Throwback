using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    // TODO: Rewire this to use values from LevelManager and display them

    private PlayerController player;
    private Text healthText;
    private Text livesText;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        //healthDisplay = GameObject.Find("healthDisplay").GetComponent<Animator>();

        healthText = GameObject.Find("healthText").GetComponent<Text>();
        healthText.text = "Health: " + player.currentHealth;
        livesText = GameObject.Find("livesText").GetComponent<Text>();
        //healthText.text = "  `: " + LevelManager.Instance.lives;
    }

    void FixedUpdate()
    {
        healthText.text = "Health: " + player.currentHealth;
        livesText.text = "Lives: " + LevelManager.Instance.lives;
    }
}
