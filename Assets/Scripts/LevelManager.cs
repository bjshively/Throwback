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

    private PlayerController player;
    public int lives;
    public int currentLevel;
    private string[] levels;
    public bool gameStarted;
    private bool gameover;
    private bool levelReady;

    public int playerLevel;
    public bool playerIsCollectingItem;

    // Scenes that aren't levels
    string[] menus = { "start", "gameover", "preroll", "credits" };
    private List<string> notLevels;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
        Setup();
        notLevels = new List<string>(menus);
    }

    void Setup()
    {
        currentLevel = 0;
        gameStarted = false;
        lives = 3;
        levelReady = false;
        playerLevel = 0;
    }

    void Start()
    {
        levels = new string[] { "start", "level00", "Level1", "credits" };
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
	
    // Update is called once per frame
    void Update()
    {
        // Start menu
        if (!gameStarted)
        {
            if (Input.GetKey("return"))
            {
                GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
                GameObject.Find("menu-startsound").GetComponent<AudioSource>().Play();
                gameStarted = true;
                Invoke("NextLevel", .25f);
            }
        }

        // In game, level has been loaded
        else if (levelReady == true)
        {
            {
                if (!player.alive)
                {
                    lives--;

                    // If you run out of lives, you lose
                    if (lives == 0 && !gameover)
                    {
                        gameover = true;
                        levelReady = false;
                        SceneManager.LoadScene("gameover");


                    }
                    else
                    {
                        player.alive = true;
                        ShowPreroll();
                    }                
                }
            }
        }
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(levels[currentLevel]);
    }
        
    // Call this whenever a level is beaten to move to the next level
    public void NextLevel()
    {
        levelReady = false;
        currentLevel++;
        ShowPreroll();
    }

    public void RestartLevel()
    {
        levelReady = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    // Do the setup of each scene
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If the scene isn't in the list "notLevels", perform play setup
        if (!notLevels.Contains(scene.name))
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            playerIsCollectingItem = false;
            // In debug mode, max out player
            if (player.debug)
            {
                playerLevel = 3;
            }
            // Reset the player state
            player.setLevel(playerLevel);
            levelReady = true;
        }
    }

    public void updateCurrentLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }


    // Display the preroll before each level start
    public void ShowPreroll()
    {
        SceneManager.LoadScene("preroll");
    }

    // Indicates whether enemies should move, etc.
    // return TRUE = stop everything
    public bool stopAllAction()
    {
        // Can easily add additional clauses to this with && conditions
        return playerIsCollectingItem;
    }

    // We may want to provide some functionality to reset the game (e.g. reset number of lives and go back to level 1)
    public void ResetGame()
    {
        Setup();
        SceneManager.LoadScene("start");
    }

}
