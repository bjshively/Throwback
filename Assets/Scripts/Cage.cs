using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    private Animator anim;
    private AudioSource audio;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    public void Unlock()
    {
        audio.PlayDelayed(.8f);
        anim.SetTrigger("fall");
        Invoke("Disable", 1);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}