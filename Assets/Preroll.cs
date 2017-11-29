using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preroll : MonoBehaviour
{
    private Text text;
    private GameObject background;
    private Canvas canvas;
    private string level;

    void Start()
    {
        canvas = transform.parent.gameObject.GetComponent<Canvas>();
        canvas.sortingLayerName = "UI";
        text = GetComponent<Text>();
        level = LevelManager.Instance.currentLevel.ToString();
        if (level == "1")
        {
            level = "Prologue";
        }
        text.text = "Level: " + level + "\n\n\n\n\nLives: " + LevelManager.Instance.lives;
        LoadLevel();
    }

    void LoadLevel()
    {
        LevelManager.Instance.Invoke("StartLevel", 3);
    }
}
