using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockReappearing : MonoBehaviour
{

    private SpriteRenderer renderer;
    private BoxCollider2D block;
    private Animator anim;
    private bool animating;

    // Use this for initialization
    void Start()
    {
        renderer = transform.parent.GetComponent<SpriteRenderer>();
        block = transform.parent.GetComponent<BoxCollider2D>();
        anim = transform.parent.GetComponent<Animator>();
        animating = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (block.enabled)
        {
            if (col.gameObject.tag == "Player" && !animating)
            {
                animating = true;
                anim.SetTrigger("break");
                Invoke("Disappear", 1f);
            }
        }
    }


    void Disappear()
    {
        anim.SetBool("die", true);
        block.enabled = false;
        // renderer.color = new Color(1f, 1f, 1f, .5f);
        Invoke("Reset", 3);

    }


    void Reset()
    {
        //renderer.color = new Color(1f, 1f, 1f, 1f);
        block.enabled = true;
        animating = false;
        anim.SetBool("die", false);

    }

    void SelfDestruct()
    {
        Destroy(transform.parent.gameObject);
    }
}
