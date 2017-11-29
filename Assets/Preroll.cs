using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preroll : MonoBehaviour
{
    private Text text;
    private GameObject background;
    private Canvas canvas;

    void Start()
    {
        canvas = transform.parent.gameObject.GetComponent<Canvas>();
        canvas.sortingLayerName = "UI";
        text = GetComponent<Text>();
        text.text = "Lives: " + LevelManager.Instance.lives;
        LoadLevel();
    }

    void LoadLevel()
    {
        LevelManager.Instance.Invoke("StartLevel", 3);
    }
}
