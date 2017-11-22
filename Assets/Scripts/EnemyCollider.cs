using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{

    private PlayerController pc;

    // Use this for initialization
    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("triggered");
        if (col.gameObject.tag == "Enemy")
        {
            pc.takeDamage();   
        }
    }
}
