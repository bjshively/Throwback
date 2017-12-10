using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    private Animator anim;
    private AudioSource audio;
    private SpriteRenderer renderer;
    private bool open;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
        open = false;
    }
	
    // Update is called once per frame
    void Update()
    {
        Unlock();
    }

    public void Unlock()
    {
        if (renderer.isVisible && open)
        {
            // Reset to false to prevent this from firing over and over again 
            open = false;
            anim.SetTrigger("fall");
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    public void setOpen()
    {
        open = true;
    }

    private void PlaySound()
    {
        audio.Play();
    }
}