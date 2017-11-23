using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReappearingBlock : MonoBehaviour
{

    private SpriteRenderer renderer;
    private BoxCollider2D block;

    // Use this for initialization
    void Start()
    {
        renderer = transform.parent.GetComponent<SpriteRenderer>();
        block = transform.parent.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Invoke("Disappear", 1.5f);
        }
    }


    void Disappear()
    {
        renderer.color = new Color(1f, 1f, 1f, .2f);
        block.enabled = false;
        Invoke("Reset", 1);

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
