using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    public void Unlock()
    {
        anim.SetTrigger("fall");
        Invoke("Disable", 1);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}