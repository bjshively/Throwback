using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public string levelToLoad;

    // Use this for initialization
    void Start()
    {
		
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    void OnTrigger2DEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
