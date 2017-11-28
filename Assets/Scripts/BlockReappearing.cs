using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockReappearing : MonoBehaviour
{

    private SpriteRenderer renderer;
    private BoxCollider2D block;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        block = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (block.enabled)
        {
            if (col.gameObject.tag == "Player")
            {
                anim.SetTrigger("die");
                Invoke("Disappear", .91f);
            }
        }
    }


    void Disappear()
    {
        block.enabled = false;
        renderer.color = new Color(1f, 1f, 1f, .5f);
        Invoke("Reset", 2);

    }


    void Reset()
    {
        renderer.color = new Color(1f, 1f, 1f, 1f);
        block.enabled = true;

    }

    void SelfDestruct()
    {
        Destroy(transform.parent.gameObject);
    }
}
