using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsReset : MonoBehaviour
{
    // private bool done;
    // Use this for initialization
    void Start()
    {
        //   done = false;
    }
	
    // Update is called once per frame
    void Update()
    {
        if (transform.TransformPoint(transform.position).y > -1)
        {
            if (Input.anyKeyDown)
            {
                LevelManager.Instance.ResetGame();
            }
        }
    }
}
