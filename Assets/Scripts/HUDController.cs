using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    // TODO: Rewire this to use values from LevelManager and display them

    private PlayerController player;
    private Text livesText;
    private Animator HUDHealth;
    private Animator HUDScope;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        livesText = GameObject.Find("livesText").GetComponent<Text>();
        HUDHealth = GameObject.Find("HUDHealth").GetComponent<Animator>();
        HUDScope = GameObject.Find("HUDScope").GetComponent<Animator>();
    }

    void Update()
    {
        HUDHealth.SetInteger("health", player.currentHealth);
        livesText.text = "Lives: " + LevelManager.Instance.lives;
        if (LevelManager.Instance.playerLevel == 3)
        {
            HUDScope.SetBool("hasScope", true);
        }
    }
}
