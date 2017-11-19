using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    private GameObject player;
    private PlayerController pc;
    private Text healthText;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();

        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthText.text = "Health: " + pc.currentHealth;
    }

    void FixedUpdate()
    {
        healthText.text = "Health: " + pc.currentHealth;
    }
}
