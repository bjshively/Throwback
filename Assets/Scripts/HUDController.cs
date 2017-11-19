using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    private GameObject player;
    private PlayerController pc;
    private Text healthText;
    private Text livesText;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();

        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthText.text = "Health: " + pc.currentHealth;
        livesText = GameObject.Find("LivesText").GetComponent<Text>();
        healthText.text = "Lives: " + pc.livesCount;
    }

    void FixedUpdate()
    {
        healthText.text = "Health: " + pc.currentHealth;
        livesText.text = "Lives: " + pc.livesCount;
    }
}
