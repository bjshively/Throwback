using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreakable : MonoBehaviour
{
    private int health = 1;

    // Use this for initialization
    void Start()
    {
        gameObject.tag = "Breakable";
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    public void Damage()
    {
        health -= 1;
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "melee")
        {
            Debug.Log("melee");
            Damage();
        }
    }
}
