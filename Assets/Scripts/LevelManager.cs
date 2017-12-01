using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private int collectedPieces;
    private int totalPieces;
    private Animator exitDoor;

    // Scenes that aren't levels, such as menus, gameover, etc.
    string[] menuStrings = { "start", "gameover", "preroll", "credits" };
    private List<string> menus;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
        SetupGame();
        menus = new List<string>(menuStrings);
        collectedPieces = 0;
        totalPieces = 0;
    }

    void Start()
    {
        levels = new string[] { "start", "Level00", "Level01", "Level02", "Level03", "Level99", "credits" };
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
            else if (Input.GetKey("space"))
            {
                SceneManager.LoadScene("credits");
                
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
                        RestartLevel();
                    }                
                }
            }
        }
    }

    public void AddPiece()
    {
        collectedPieces++;
        exitDoor = GameObject.Find("ExitDoor").GetComponent<Animator>();
        exitDoor.SetInteger("pieces", collectedPieces);
        if (collectedPieces == 7)
        {
            exitDoor.SetBool("complete", true);
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

        // Save any collected pieces once you beat the level
        totalPieces = collectedPieces;
        ShowPreroll();
    }

    public void RestartLevel()
    {
        // Reset the pieces count if you die
        collectedPieces = totalPieces;
        levelReady = false;
        ShowPreroll();
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
        SetupGame();
        SceneManager.LoadScene("start");
    }

    // Set the initial conditions for the game
    void SetupGame()
    {
        gameover = false;
        gameStarted = false;
        currentLevel = 0;
        levelReady = false;
        lives = 5;
        playerLevel = 0;
        playerIsCollectingItem = false;
        collectedPieces = 0;
        totalPieces = 0;
    }

    // Set the initial conditions for a level
    void SetupLevel()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        exitDoor = GameObject.Find("ExitDoor").GetComponent<Animator>();
        exitDoor.SetInteger("pieces", totalPieces);
        playerIsCollectingItem = false;

        // In debug mode, max out player
        if (player.debug)
        {
            playerLevel = 3;
        }
        else
        {
            player.setLevel(playerLevel);    
        }
        // Reset the player state

        player.alive = true;
        levelReady = true;
    }

    // Setup each playable scene
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If the scene isn't in the list "notLevels", perform play setup
        if (levels.Contains(scene.name))
        {
            SetupLevel();
        }
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("credits");
    }
}
