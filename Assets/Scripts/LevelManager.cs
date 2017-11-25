using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // This is a basic level manager. It persists between scene loads as written.
    // Should be added to an empty game object in the first scene.
    // Can be accessed globally via LevelManager.Instance.x();

    public static LevelManager Instance { set; get; }

    public string levelToLoad;
    private int lives;
    private PlayerController player;

    private int currentLevel;

    //TODO: Create array of levels to be loaded
    //    SceneManager.LoadScene("Level1");

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
        currentLevel = 0;
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
	
    // Update is called once per frame
    void Update()
    {
        lives = player.livesCount;
        Debug.Log(lives);
    }

    public void Win()
    {
        Debug.Log("Victory");
    }
    
}
